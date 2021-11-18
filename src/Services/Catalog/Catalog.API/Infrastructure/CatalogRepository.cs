using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
        EntityEntry<CatalogItem>? entityEntry = await _context.CatalogItems.AddAsync(item);
        await _context.SaveChangesAsync();

        return entityEntry?.Entity ?? null;
    }

    public async Task<CatalogItem?> UpdateAsync(CatalogItem item)
    {
        if (item is null)
        {
            return null;
        }

        CatalogItem? existingItem = await _context.CatalogItems.AsNoTracking().SingleOrDefaultAsync(p => p.Id == item.Id);

        if (existingItem is null)
        {
            return null;
        }

        _context.CatalogItems.Update(item);
        await _context.SaveChangesAsync();

        return item;
    }

    public async Task<CatalogItem?> DeleteAsync(long id)
    {
        CatalogItem? itemToDelete = await _context.CatalogItems.FindAsync(id);

        if (itemToDelete is null)
        {
            return null;
        }

        _context.CatalogItems.Remove(itemToDelete);
        await _context.SaveChangesAsync();

        return itemToDelete;
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
