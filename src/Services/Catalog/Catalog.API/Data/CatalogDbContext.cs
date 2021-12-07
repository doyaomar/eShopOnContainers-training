namespace Catalog.API.Data;

public class CatalogDbContext : ICatalogDbContext
{
    public IMongoCollection<CatalogItem> CatalogItems { get; }

    public IMongoCollection<CatalogBrand> CatalogBrands { get; }

    public IMongoCollection<CatalogType> CatalogTypes { get; }

    public CatalogDbContext(IMongoClient mongoClient, IOptions<CatalogDbSettings> settings)
    {
        var catalogDbSettings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        _ = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));
        var db = mongoClient.GetDatabase(catalogDbSettings.DatabaseName);
        
        CatalogItems = db.GetCollection<CatalogItem>(catalogDbSettings.CatalogItemsCollectionName);
        CatalogBrands = db.GetCollection<CatalogBrand>(catalogDbSettings.CatalogBrandsCollectionName);
        CatalogTypes = db.GetCollection<CatalogType>(catalogDbSettings.CatalogTypesCollectionName);
    }

    public async Task<Guid> InsertOneAsync(CatalogItem item, CancellationToken cancellationToken = default)
    {
        await CatalogItems.InsertOneAsync(item, null, cancellationToken);

        return item.Id;
    }

    public async Task<CatalogItem?> FindOneAndReplaceAsync(CatalogItem item, CancellationToken cancellationToken = default)
    {
        _ = item ?? throw new ArgumentNullException(nameof(item));

        return await CatalogItems.FindOneAndReplaceAsync(x => x.Id == item.Id, item, null, cancellationToken);
    }

    public async Task<CatalogItem?> FindOneAndDeleteAsync(Guid id, CancellationToken cancellationToken = default)
    => await CatalogItems.FindOneAndDeleteAsync(x => x.Id == id, null, cancellationToken);

    public async Task<CatalogItem?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    => await CatalogItems.Find(x => x.Id == id).SingleOrDefaultAsync(cancellationToken);
}
