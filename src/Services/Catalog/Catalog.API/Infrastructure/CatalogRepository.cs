using System;
using System.Threading.Tasks;
using Catalog.API.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Catalog.API.Infrastructure
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly CatalogContext _context;

        public CatalogRepository(CatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<CatalogItem> GetAsync(long id)
        {
            return await _context.CatalogItems.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<CatalogItem>> GetAllAsync(int pageSize, int pageIndex)
        {
            return await _context
            .CatalogItems
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();
        }

        public async Task<IEnumerable<CatalogItem>> GetByIdsAsync(IEnumerable<long> ids)
        {
            return await _context
            .CatalogItems
            .Where(c => ids.Contains(c.Id))
            .ToListAsync();
        }

        public async Task<long> GetCountAsync()
        {
            return await _context.CatalogItems.LongCountAsync();
        }

        public async Task<long> CreateAsync(CatalogItem item)
        {
            await _context.CatalogItems.AddAsync(item);

            return item.Id;
        }

        public void Update(CatalogItem item)
        {
            _context.CatalogItems.Update(item);
        }

        public void Delete(CatalogItem item)
        {
            _context.CatalogItems.Remove(item);
        }
    }
}