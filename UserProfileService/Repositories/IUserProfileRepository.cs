using System.Collections.Generic;
using System.Threading.Tasks;
using UserProfileService.Models;

namespace UserProfileService.Repositories
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> GetUserProfileAsync(int id);
        Task<UserProfile> GetUserProfileByUsernameAsync(string username);
        Task<IEnumerable<UserProfile>> GetAllUserProfilesAsync();
        Task CreateUserProfileAsync(UserProfile userProfile);
        Task UpdateUserProfileAsync(UserProfile userProfile);
        Task DeleteUserProfileAsync(int id);
    }
}