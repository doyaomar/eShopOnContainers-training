namespace Catalog.API.Data;

public class CatalogDbContext
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
}
