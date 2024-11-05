using ImportExportService.DTOs;
using System.Threading.Tasks;

namespace ImportExportService.Services
{
    public interface IExportService
    {
        Task<ExportDTO?> ExportDataAsync(int id);
    }
}
