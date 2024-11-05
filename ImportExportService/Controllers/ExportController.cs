using ImportExportService.DTOs;
using ImportExportService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ImportExportService.Controllers
{
    [Route("api/export")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private readonly IExportService _exportService;

        public ExportController(IExportService exportService)
        {
            _exportService = exportService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ExportData(int id)
        {
            var result = await _exportService.ExportDataAsync(id);
            return result != null ? Ok(result) : NotFound("Data not found.");
        }
    }
}
