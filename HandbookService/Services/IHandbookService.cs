using HandbookService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandbookService.Services
{
    public interface IHandbookManager
    {
        Task<IEnumerable<HandbookItem>> GetAllItemsAsync();
        Task<HandbookItem?> GetItemByIdAsync(int id);
        Task AddItemAsync(HandbookItem item);
        Task UpdateItemAsync(HandbookItem item);
        Task DeleteItemAsync(int id);
    }
}
