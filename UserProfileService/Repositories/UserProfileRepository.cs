using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserProfileService.Data;
using UserProfileService.Models;

namespace UserProfileService.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly UserProfileDbContext _context;

        public UserProfileRepository(UserProfileDbContext context)
        {
            _context = context;
        }

        public async Task<UserProfile> GetUserProfileAsync(int id) =>
            await _context.UserProfiles.FindAsync(id);

        public async Task<UserProfile> GetUserProfileByUsernameAsync(string username) =>
            await _context.UserProfiles.FirstOrDefaultAsync(u => u.Username == username);

        public async Task<IEnumerable<UserProfile>> GetAllUserProfilesAsync() =>
            await _context.UserProfiles.ToListAsync();

        public async Task CreateUserProfileAsync(UserProfile userProfile)
        {
            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserProfileAsync(UserProfile userProfile)
        {
            _context.UserProfiles.Update(userProfile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserProfileAsync(int id)
        {
            var userProfile = await GetUserProfileAsync(id);
            if (userProfile != null)
            {
                _context.UserProfiles.Remove(userProfile);
                await _context.SaveChangesAsync();
            }
        }
    }
}