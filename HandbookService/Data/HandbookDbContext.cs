using Microsoft.EntityFrameworkCore;
using HandbookService.Models;

namespace HandbookService.Data
{
    public class HandbookDbContext : DbContext
    {
        public HandbookDbContext(DbContextOptions<HandbookDbContext> options) : base(options) { }

        public DbSet<HandbookItem> HandbookItems { get; set; }
    }
}
