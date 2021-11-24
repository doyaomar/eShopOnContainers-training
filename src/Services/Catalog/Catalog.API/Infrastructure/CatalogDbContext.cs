namespace Catalog.API.Infrastructure;

public class CatalogDbContext : ICatalogDbContext
{
    private readonly IMongoCollection<CatalogItem> _catalogItemCollection;

    public CatalogDbContext(IOptions<CatalogDbSettings> settings)
    {
        var catalogDbSettings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        var client = new MongoClient(catalogDbSettings.ConnectionString);
        var database = client.GetDatabase(catalogDbSettings.DatabaseName);
        _catalogItemCollection = database.GetCollection<CatalogItem>(catalogDbSettings.CatalogItemsCollectionName);
    }

    public async Task<CatalogItem?> CreateAsync(CatalogItem item)
    {
        await _catalogItemCollection.InsertOneAsync(item);

        return item;
    }

    public async Task<CatalogItem?> UpdateAsync(CatalogItem item)
    {
        if (item is null)
        {
            return null;
        }

        return await _catalogItemCollection.FindOneAndReplaceAsync(x => x.Id == item.Id, item);
    }

    public async Task<CatalogItem?> DeleteAsync(Guid id) => await _catalogItemCollection.FindOneAndDeleteAsync(x => x.Id == id);

    public async Task<CatalogItem?> GetAsync(Guid id) => await _catalogItemCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

}
