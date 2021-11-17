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

    public async Task<CatalogItem?> CreateAsync(CatalogItem item)
    {
        var createdItem = await _context.CatalogItems.AddAsync(item);
        await _context.SaveChangesAsync();

        return createdItem?.Entity ?? null;
    }

    public async Task<CatalogItem?> UpdateAsync(CatalogItem item)
    {
        if (item is null)
        {
            return null;
        }

        var catalogItem = await _context.CatalogItems.AsNoTracking().SingleOrDefaultAsync(p => p.Id == item.Id);

        if (catalogItem is null)
        {
            return null;
        }

        _context.CatalogItems.Update(item);
        await _context.SaveChangesAsync();

        return item;
    }

    public async Task<CatalogItem?> DeleteAsync(long id)
    {
        var item = await _context.CatalogItems.FindAsync(id);

        if (item is null)
        {
            return null;
        }

        _context.CatalogItems.Remove(item);
        await _context.SaveChangesAsync();

        return item;
    }

    public async Task<CatalogItem?> GetAsync(long id)
    {
        return await _context.
        CatalogItems
        .AsNoTracking()
        .Include(c => c.CatalogBrand)
        .Include(c => c.CatalogType)
        .SingleOrDefaultAsync(c => c.Id == id);
    }
}
