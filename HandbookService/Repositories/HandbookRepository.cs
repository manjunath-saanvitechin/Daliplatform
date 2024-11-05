using HandbookService.Data;
using HandbookService.Models;
using Microsoft.EntityFrameworkCore;

namespace HandbookService.Repositories
{
    public class HandbookRepository : IHandbookRepository
    {
        private readonly HandbookDbContext _context;

        public HandbookRepository(HandbookDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HandbookItem>> GetAllItemsAsync()
        {
            return await _context.HandbookItems.ToListAsync();
        }

        public async Task<HandbookItem?> GetItemByIdAsync(int id)
        {
            return await _context.HandbookItems.FindAsync(id);
        }

        public async Task AddItemAsync(HandbookItem item)
        {
            _context.HandbookItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(HandbookItem item)
        {
            _context.HandbookItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await _context.HandbookItems.FindAsync(id);
            if (item != null)
            {
                _context.HandbookItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
