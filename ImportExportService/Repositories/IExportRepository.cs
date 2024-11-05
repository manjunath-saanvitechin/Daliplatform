using ImportExportService.Models;
using System.Threading.Tasks;

namespace ImportExportService.Repositories
{
    public interface IExportRepository
    {
        Task<ExportRecord?> GetExportDataAsync(int id);
    }
}
