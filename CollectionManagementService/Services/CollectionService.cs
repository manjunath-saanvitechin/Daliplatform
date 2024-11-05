using CollectionManagementService.DTOs;
using CollectionManagementService.Models;
using CollectionManagementService.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace CollectionManagementService.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository _repository;

        public CollectionService(ICollectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CollectionDTO>> GetAllCollectionsAsync()
        {
            var collections = await _repository.GetAllCollectionsAsync();
            return collections.Select(c => new CollectionDTO { Id = c.Id, Name = c.Name, Description = c.Description });
        }

        public async Task<CollectionDTO?> GetCollectionByIdAsync(int id)
        {
            var collection = await _repository.GetCollectionByIdAsync(id);
            return collection == null ? null : new CollectionDTO { Id = collection.Id, Name = collection.Name, Description = collection.Description };
        }

        public async Task<CollectionDTO> CreateCollectionAsync(CollectionDTO collectionDto)
        {
            var collection = new Collection { Name = collectionDto.Name, Description = collectionDto.Description };
            collection = await _repository.AddCollectionAsync(collection);
            return new CollectionDTO { Id = collection.Id, Name = collection.Name, Description = collection.Description };
        }

        public async Task<bool> UpdateCollectionAsync(int id, CollectionDTO collectionDto)
        {
            var collection = await _repository.GetCollectionByIdAsync(id);
            if (collection == null) return false;

            collection.Name = collectionDto.Name;
            collection.Description = collectionDto.Description;

            return await _repository.UpdateCollectionAsync(collection);
        }

        public async Task<bool> DeleteCollectionAsync(int id)
        {
            return await _repository.DeleteCollectionAsync(id);
        }
    }
}
