namespace Catalog.API.Infrastructure.Data;

public class CatalogRepository : ICatalogRepository
{
    private readonly CatalogDbContext _context;

    public CatalogRepository(CatalogDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CatalogItem> CreateAsync(CatalogItem item)
    {
        await _context.CatalogItems.InsertOneAsync(item);

        return item;
    }

    public async Task<CatalogItem?> UpdateAsync(CatalogItem item)
    {
        if (item is null)
        {
            return null;
        }

        return await _context.CatalogItems.FindOneAndReplaceAsync(x => x.Id == item.Id, item);
    }

    public async Task<CatalogItem?> DeleteAsync(Guid id) => await _context.CatalogItems.FindOneAndDeleteAsync(x => x.Id == id);

    public async Task<CatalogItem?> GetAsync(Guid id) => await _context.CatalogItems.Find(x => x.Id == id).FirstOrDefaultAsync();
}