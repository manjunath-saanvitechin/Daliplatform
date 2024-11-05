using CollectionManagementService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollectionManagementService.Repositories
{
    public interface ICollectionRepository
    {
        Task<IEnumerable<Collection>> GetAllCollectionsAsync();
        Task<Collection?> GetCollectionByIdAsync(int id);
        Task<Collection> AddCollectionAsync(Collection collection);
        Task<bool> UpdateCollectionAsync(Collection collection);
        Task<bool> DeleteCollectionAsync(int id);
    }
}
