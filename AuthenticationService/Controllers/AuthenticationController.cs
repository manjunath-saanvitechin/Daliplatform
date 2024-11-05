using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Models;
using AuthenticationService.Repositories;
using AuthenticationService.Services;

namespace AuthenticationService.Controllers
{
    [Route("api/{tenantId}/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly ITenantService _tenantService;

        public AuthenticationController(IAuthService authService, IUserRepository userRepository, ITenantService tenantService)
        {
            _authService = authService;
            _userRepository = userRepository;
            _tenantService = tenantService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string tenantId, [FromBody] RegisterModel model)
        {
            try
            {
                var token = await _authService.RegisterUserAsync(model, tenantId);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string tenantId, [FromBody] LoginModel model)
        {
            try
            {
                var token = await _authService.AuthenticateUserAsync(model, tenantId);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
        }

        [HttpGet("profile/{username}")]
        public async Task<IActionResult> GetProfile(string tenantId, string username)
        {
            try
            {
                var user = await _authService.GetUserProfileAsync(username, tenantId);
                if (user == null) return NotFound(new { Message = "User not found" });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("profile/{username}")]
        public async Task<IActionResult> UpdateProfile(string tenantId, string username, [FromBody] UpdateProfileModel model)
        {
            try
            {
                var user = await _authService.GetUserProfileAsync(username, tenantId);
                if (user == null) return NotFound(new { Message = "User not found" });

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Role = model.Role;

                await _authService.UpdateUserProfileAsync(user, tenantId);
                return Ok(new { Message = "Profile updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("profile/{username}")]
        public async Task<IActionResult> DeleteProfile(string tenantId, string username)
        {
            try
            {
                var user = await _authService.GetUserProfileAsync(username, tenantId);
                if (user == null) return NotFound(new { Message = "User not found" });

                await _authService.DeleteUserProfileAsync(username, tenantId);
                return Ok(new { Message = "Profile deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("register-tenant")]
        public async Task<IActionResult> RegisterTenant([FromBody] TenantModel model)
        {
            try
            {
                var tenantId = await _tenantService.RegisterTenantAsync(model);
                return Ok(new { TenantId = tenantId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
