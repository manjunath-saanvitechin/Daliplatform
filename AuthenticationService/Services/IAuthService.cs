using System.Threading.Tasks;
using AuthenticationService.Models;

namespace AuthenticationService.Services
{
    public interface IAuthService
    {
        // Registration and Authentication with Tenant Support
        Task<string> RegisterUserAsync(RegisterModel model, string tenantId);
        Task<string> AuthenticateUserAsync(LoginModel model, string tenantId);

        // Profile management methods with Tenant Support
        Task<AppUser> GetUserProfileAsync(string username, string tenantId);
        Task UpdateUserProfileAsync(AppUser updatedUser, string tenantId);
        Task DeleteUserProfileAsync(string username, string tenantId);
    }
}
