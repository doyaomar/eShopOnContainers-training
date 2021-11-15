using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Infrastructure;

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

    public async Task<CatalogItem> CreateAsync(CatalogItem item)
    {
        var createdItem = await _context.CatalogItems.AddAsync(item);

        return createdItem.Entity;
    }

    public void Update(CatalogItem item)
    {
        _context.CatalogItems.Update(item);
    }

    public void Delete(CatalogItem item)
    {
        _context.CatalogItems.Remove(item);
    }

    public async Task<CatalogItem> GetAsync(long id, bool asNoTracking = false)
    {
        return asNoTracking ?
            await _context.CatalogItems.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id)
            : await _context.CatalogItems.SingleOrDefaultAsync(c => c.Id == id);
    }
}
