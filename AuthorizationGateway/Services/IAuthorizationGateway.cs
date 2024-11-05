using System.Threading.Tasks;

namespace AuthorizationGateway.Services
{
    public interface IAuthorizationGateway
    {
        Task<string> AuthenticateAsync(string username, string password);
    }
}
