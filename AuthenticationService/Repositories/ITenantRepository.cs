using System;
using System.Threading.Tasks;
using AuthenticationService.Models;

namespace AuthenticationService.Repositories
{
    public interface ITenantRepository
    {
        Task CreateTenantAsync(Tenant tenant);
        Task<Tenant> GetTenantByIdAsync(Guid tenantId);
    }
}
