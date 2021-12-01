namespace Catalog.API.Data;

public interface ICatalogDbContext
{
    IMongoCollection<CatalogItem> CatalogItems { get; }
    IMongoCollection<CatalogBrand> CatalogBrands { get; }
    IMongoCollection<CatalogType> CatalogTypes { get; }
    Task<CatalogItem?> FindAsync(Guid id, CancellationToken cancellationToken = default);
}