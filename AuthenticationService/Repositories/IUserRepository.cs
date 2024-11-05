using System.Threading.Tasks;
using AuthenticationService.Models;

namespace AuthenticationService.Repositories
{
    public interface IUserRepository
    {
        Task<AppUser> GetUserByUsernameAsync(string username, string tenantId);
        Task CreateUserAsync(AppUser user);
        Task UpdateUserAsync(AppUser user); 
        Task DeleteUserAsync(AppUser user); 
    }
}
