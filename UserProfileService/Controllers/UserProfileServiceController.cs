using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserProfileService.Models;
using UserProfileService.Services;

namespace UserProfileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _service;

        public UserProfileController(IUserProfileService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfile>> Get(int id)
        {
            var profile = await _service.GetUserProfileAsync(id);
            if (profile == null) return NotFound();
            return profile;
        }

        [HttpGet("current")]
        public async Task<ActionResult<UserProfile>> GetCurrentProfile()
        {
            var username = User.Identity.Name;
            var profile = await _service.GetUserProfileByUsernameAsync(username);
            if (profile == null) return NotFound();
            return profile;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetAll()
        {
            var profiles = await _service.GetAllUserProfilesAsync();
            return Ok(profiles);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(UserProfile userProfile)
        {
            await _service.CreateUserProfileAsync(userProfile);
            return CreatedAtAction(nameof(Get), new { id = userProfile.Id }, userProfile);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserProfile userProfile)
        {
            if (id != userProfile.Id) return BadRequest();
            await _service.UpdateUserProfileAsync(userProfile);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteUserProfileAsync(id);
            return NoContent();
        }
    }
}
