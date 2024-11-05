using System;
using System.Threading.Tasks;
using AuthenticationService.Data;
using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly AuthDbContext _context;

        public TenantRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task CreateTenantAsync(Tenant tenant)
        {
            await _context.Tenants.AddAsync(tenant);
            await _context.SaveChangesAsync();
        }

        public async Task<Tenant> GetTenantByIdAsync(Guid tenantId)
        {
            return await _context.Tenants.SingleOrDefaultAsync(t => t.TenantId == tenantId);
        }
    }
}
