using ImportExportService.Models;
using System.Threading.Tasks;

namespace ImportExportService.Repositories
{
    public interface IImportRepository
    {
        Task SaveImportDataAsync(ImportRecord importRecord);
    }
}
