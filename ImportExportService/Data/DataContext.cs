using ImportExportService.Models;
using Microsoft.EntityFrameworkCore;

namespace ImportExportService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<ImportRecord> ImportRecords { get; set; } = null!;
        public DbSet<ExportRecord> ExportRecords { get; set; } = null!;
    }
}
