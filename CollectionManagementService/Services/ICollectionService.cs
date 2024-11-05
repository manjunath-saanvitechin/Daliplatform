using CollectionManagementService.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollectionManagementService.Services
{
    public interface ICollectionService
    {
        Task<IEnumerable<CollectionDTO>> GetAllCollectionsAsync();
        Task<CollectionDTO?> GetCollectionByIdAsync(int id);
        Task<CollectionDTO> CreateCollectionAsync(CollectionDTO collectionDto);
        Task<bool> UpdateCollectionAsync(int id, CollectionDTO collectionDto);
        Task<bool> DeleteCollectionAsync(int id);
    }
}
