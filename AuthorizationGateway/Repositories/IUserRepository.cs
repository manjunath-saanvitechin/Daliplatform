using AuthorizationGateway.Models;
using System.Threading.Tasks;

namespace AuthorizationGateway.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserByUsernameAsync(string username);
        Task AddUserAsync(UserModel user);
    }
}
