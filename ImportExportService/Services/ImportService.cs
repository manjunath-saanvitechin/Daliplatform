using ImportExportService.DTOs;
using ImportExportService.Models;
using ImportExportService.Repositories;
using System.Threading.Tasks;

namespace ImportExportService.Services
{
    public class ImportService : IImportService
    {
        private readonly IImportRepository _importRepository;

        public ImportService(IImportRepository importRepository)
        {
            _importRepository = importRepository;
        }

        public async Task ImportDataAsync(ImportDTO importDto)
        {
            var importRecord = new ImportRecord { Id = importDto.Id, Data = importDto.Data };
            await _importRepository.SaveImportDataAsync(importRecord);
        }
    }
}
