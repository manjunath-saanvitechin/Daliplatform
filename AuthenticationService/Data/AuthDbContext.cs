// Data/AuthDbContext.cs
using Microsoft.EntityFrameworkCore;
using AuthenticationService.Models;

namespace AuthenticationService.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Tenant> Tenants { get; set; }  // Optional: Tenant table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().HasIndex(u => new { u.Username, u.TenantId }).IsUnique();
        }
    }
}
