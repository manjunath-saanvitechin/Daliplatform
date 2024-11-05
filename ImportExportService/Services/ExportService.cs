using ImportExportService.DTOs;
using ImportExportService.Models;
using ImportExportService.Repositories;
using System.Threading.Tasks;

namespace ImportExportService.Services
{
    public class ExportService : IExportService
    {
        private readonly IExportRepository _exportRepository;

        public ExportService(IExportRepository exportRepository)
        {
            _exportRepository = exportRepository;
        }

        public async Task<ExportDTO?> ExportDataAsync(int id)
        {
            var exportRecord = await _exportRepository.GetExportDataAsync(id);
            return exportRecord == null ? null : new ExportDTO { Id = exportRecord.Id, ExportData = exportRecord.ExportData };
        }
    }
}
