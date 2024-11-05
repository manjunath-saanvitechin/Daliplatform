using ImportExportService.DTOs;
using System.Threading.Tasks;

namespace ImportExportService.Services
{
    public interface IImportService
    {
        Task ImportDataAsync(ImportDTO importDto);
    }
}
