using HandbookService.Models;
using HandbookService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HandbookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandbookController : ControllerBase
    {
        private readonly IHandbookManager _manager;

        public HandbookController(IHandbookManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _manager.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            var item = await _manager.GetItemByIdAsync(id);
            return item != null ? Ok(item) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(HandbookItem item)
        {
            await _manager.AddItemAsync(item);
            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, HandbookItem item)
        {
            if (id != item.Id) return BadRequest();
            await _manager.UpdateItemAsync(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _manager.DeleteItemAsync(id);
            return NoContent();
        }
    }
}
