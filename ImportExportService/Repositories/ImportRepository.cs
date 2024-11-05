using ImportExportService.Data;
using ImportExportService.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ImportExportService.Repositories
{
    public class ImportRepository : IImportRepository
    {
        private readonly DataContext _context;

        public ImportRepository(DataContext context)
        {
            _context = context;
        }

        public async Task SaveImportDataAsync(ImportRecord importRecord)
        {
            _context.ImportRecords.Add(importRecord);
            await _context.SaveChangesAsync();
        }
    }
}
