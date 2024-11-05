using CollectionManagementService.Models;
using Microsoft.EntityFrameworkCore;

namespace CollectionManagementService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Collection> Collections { get; set; }
    }
}
