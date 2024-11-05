using Microsoft.AspNetCore.Mvc;
using CollectionManagementService.Services;
using CollectionManagementService.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CollectionManagementService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectionDTO>>> GetAllCollections()
        {
            var collections = await _collectionService.GetAllCollectionsAsync();
            return Ok(collections);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CollectionDTO>> GetCollectionById(int id)
        {
            var collection = await _collectionService.GetCollectionByIdAsync(id);
            if (collection == null)
            {
                return NotFound();
            }
            return Ok(collection);
        }

        [HttpPost]
        public async Task<ActionResult<CollectionDTO>> CreateCollection(CollectionDTO collectionDto)
        {
            var createdCollection = await _collectionService.CreateCollectionAsync(collectionDto);
            return CreatedAtAction(nameof(GetCollectionById), new { id = createdCollection.Id }, createdCollection);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCollection(int id, CollectionDTO collectionDto)
        {
            if (!await _collectionService.UpdateCollectionAsync(id, collectionDto))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollection(int id)
        {
            if (!await _collectionService.DeleteCollectionAsync(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
