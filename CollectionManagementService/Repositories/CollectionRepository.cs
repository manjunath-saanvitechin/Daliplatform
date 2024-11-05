using CollectionManagementService.Models;
using CollectionManagementService.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollectionManagementService.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly DataContext _context;

        public CollectionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Collection>> GetAllCollectionsAsync()
        {
            return await _context.Collections.ToListAsync();
        }

        public async Task<Collection?> GetCollectionByIdAsync(int id)
        {
            return await _context.Collections.FindAsync(id);
        }

        public async Task<Collection> AddCollectionAsync(Collection collection)
        {
            _context.Collections.Add(collection);
            await _context.SaveChangesAsync();
            return collection;
        }

        public async Task<bool> UpdateCollectionAsync(Collection collection)
        {
            _context.Collections.Update(collection);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCollectionAsync(int id)
        {
            var collection = await _context.Collections.FindAsync(id);
            if (collection == null) return false;

            _context.Collections.Remove(collection);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
