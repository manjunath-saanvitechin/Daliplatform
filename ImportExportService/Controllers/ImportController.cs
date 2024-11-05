using ImportExportService.DTOs;
using ImportExportService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ImportExportService.Controllers
{
    [Route("api/import")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly IImportService _importService;

        public ImportController(IImportService importService)
        {
            _importService = importService;
        }

        [HttpPost]
        public async Task<IActionResult> ImportData([FromBody] ImportDTO importDto)
        {
            await _importService.ImportDataAsync(importDto);
            return Ok("Data imported successfully.");
        }
    }
}
