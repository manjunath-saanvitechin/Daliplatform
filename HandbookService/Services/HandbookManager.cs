using HandbookService.Models;
using HandbookService.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandbookService.Services
{
    public class HandbookManager : IHandbookManager
    {
        private readonly IHandbookRepository _repository;

        public HandbookManager(IHandbookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<HandbookItem>> GetAllItemsAsync()
        {
            return await _repository.GetAllItemsAsync();
        }

        public async Task<HandbookItem?> GetItemByIdAsync(int id)
        {
            return await _repository.GetItemByIdAsync(id);
        }

        public async Task AddItemAsync(HandbookItem item)
        {
            await _repository.AddItemAsync(item);
        }

        public async Task UpdateItemAsync(HandbookItem item)
        {
            await _repository.UpdateItemAsync(item);
        }

        public async Task DeleteItemAsync(int id)
        {
            await _repository.DeleteItemAsync(id);
        }
    }
}
