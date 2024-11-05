using HandbookService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandbookService.Repositories
{
    public interface IHandbookRepository
    {
        Task<IEnumerable<HandbookItem>> GetAllItemsAsync();
        Task<HandbookItem?> GetItemByIdAsync(int id);
        Task AddItemAsync(HandbookItem item);
        Task UpdateItemAsync(HandbookItem item);
        Task DeleteItemAsync(int id);
    }
}
