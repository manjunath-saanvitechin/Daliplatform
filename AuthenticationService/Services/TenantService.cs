using System;
using System.Threading.Tasks;
using AuthenticationService.Models;
using AuthenticationService.Repositories;

namespace AuthenticationService.Services
{
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;

        public TenantService(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<Guid> RegisterTenantAsync(TenantModel model)
        {
            var tenant = new Tenant
            {
                TenantName = model.TenantName,
                Domain = model.Domain,
                CreatedAt = DateTime.UtcNow
            };

            await _tenantRepository.CreateTenantAsync(tenant);
            return tenant.TenantId;
        }

        public async Task<Tenant> GetTenantByIdAsync(Guid tenantId)
        {
            return await _tenantRepository.GetTenantByIdAsync(tenantId);
        }
    }
}
