using System;
using System.Threading.Tasks;
using AuthenticationService.Models;

namespace AuthenticationService.Services
{
    public interface ITenantService
    {
        Task<Guid> RegisterTenantAsync(TenantModel model);
        Task<Tenant> GetTenantByIdAsync(Guid tenantId);
    }
}
