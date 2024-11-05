using Microsoft.EntityFrameworkCore;
using AuthorizationGateway.Models;

namespace AuthorizationGateway.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
    }
}
