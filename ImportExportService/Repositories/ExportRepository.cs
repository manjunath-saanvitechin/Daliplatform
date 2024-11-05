using ImportExportService.Data;
using ImportExportService.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ImportExportService.Repositories
{
    public class ExportRepository : IExportRepository
    {
        private readonly DataContext _context;

        public ExportRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ExportRecord?> GetExportDataAsync(int id)
        {
            return await _context.ExportRecords.FindAsync(id);
        }
    }
}
