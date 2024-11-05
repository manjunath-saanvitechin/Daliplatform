using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;
using AuthenticationService.Models;
using AuthenticationService.Repositories;
using Microsoft.Extensions.Configuration;
using Azure.Identity;
using Microsoft.Graph.Models;
using Azure.Core;

namespace AuthenticationService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly GraphServiceClient _graphClient;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;

            var clientSecretCredential = new ClientSecretCredential(
                _configuration["AzureAd:TenantId"],
                _configuration["AzureAd:ClientId"],
                _configuration["AzureAd:ClientSecret"]
            );

            _graphClient = new GraphServiceClient(clientSecretCredential);
        }

        public async Task<string> RegisterUserAsync(RegisterModel model, string tenantId)
        {
            // Register in Azure AD with TenantId
            var passwordProfile = new PasswordProfile
            {
                ForceChangePasswordNextSignIn = false,
                Password = model.Password
            };

            var azureUser = new User
            {
                AccountEnabled = true,
                DisplayName = $"{model.FirstName} {model.LastName}",
                MailNickname = model.Username,
                UserPrincipalName = $"{model.Username}@{_configuration["AzureAd:Domain"]}",
                PasswordProfile = passwordProfile,
                GivenName = model.FirstName,
                Surname = model.LastName
            };

            await _graphClient.Users.PostAsync(azureUser);

            // Register in Local Database
            var newUser = new AppUser
            {
                Username = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Role = model.Role,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                TenantId = tenantId, // Set TenantId
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.CreateUserAsync(newUser);
            return GenerateJwtToken(newUser);
        }

        public async Task<string> AuthenticateUserAsync(LoginModel model, string tenantId)
        {
            try
            {
                var clientSecretCredential = new ClientSecretCredential(
                    _configuration["AzureAd:TenantId"],
                    _configuration["AzureAd:ClientId"],
                    _configuration["AzureAd:ClientSecret"]
                );

                var authResult = await clientSecretCredential.GetTokenAsync(
                    new TokenRequestContext(new[] { "https://graph.microsoft.com/.default" })
                );

                return authResult.Token;
            }
            catch
            {
                var user = await _userRepository.GetUserByUsernameAsync(model.Username, tenantId); // Check tenant
                if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                    throw new Exception("Invalid credentials");

                return GenerateJwtToken(user);
            }
        }

        public async Task<AppUser> GetUserProfileAsync(string username, string tenantId)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username, tenantId); // Check tenant
            if (user == null)
                throw new Exception("User not found");

            return user;
        }

        public async Task UpdateUserProfileAsync(AppUser updatedUser, string tenantId)
        {
            var user = await _userRepository.GetUserByUsernameAsync(updatedUser.Username, tenantId); // Check tenant
            if (user == null)
                throw new Exception("User not found");

            user.Email = updatedUser.Email;
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Role = updatedUser.Role;

            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserProfileAsync(string username, string tenantId)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username, tenantId); // Check tenant
            if (user == null)
                throw new Exception("User not found");

            await _userRepository.DeleteUserAsync(user);
        }

        private string GenerateJwtToken(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("TenantId", user.TenantId)  // Include TenantId in token
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
