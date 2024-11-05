using System.Collections.Generic;
using System.Threading.Tasks;
using UserProfileService.Models;
using UserProfileService.Repositories;

namespace UserProfileService.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _repository;

        public UserProfileService(IUserProfileRepository repository)
        {
            _repository = repository;
        }

        public Task<UserProfile> GetUserProfileAsync(int id) => _repository.GetUserProfileAsync(id);

        public Task<UserProfile> GetUserProfileByUsernameAsync(string username) => _repository.GetUserProfileByUsernameAsync(username);

        public Task<IEnumerable<UserProfile>> GetAllUserProfilesAsync() => _repository.GetAllUserProfilesAsync();

        public Task CreateUserProfileAsync(UserProfile userProfile) => _repository.CreateUserProfileAsync(userProfile);

        public Task UpdateUserProfileAsync(UserProfile userProfile) => _repository.UpdateUserProfileAsync(userProfile);

        public Task DeleteUserProfileAsync(int id) => _repository.DeleteUserProfileAsync(id);
    }
}