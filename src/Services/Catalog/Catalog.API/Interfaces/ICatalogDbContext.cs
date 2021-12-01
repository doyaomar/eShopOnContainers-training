namespace Catalog.API.Interfaces;

public interface ICatalogDbContext
{
    IMongoCollection<CatalogItem> CatalogItems { get; }
    IMongoCollection<CatalogBrand> CatalogBrands { get; }
    IMongoCollection<CatalogType> CatalogTypes { get; }
    Task<Guid> InsertOneAsync(CatalogItem item, CancellationToken cancellationToken = default);
    Task<CatalogItem?> FindOneAndDeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<CatalogItem?> FindOneAndReplaceAsync(CatalogItem item, CancellationToken cancellationToken = default);
    Task<CatalogItem?> FindAsync(Guid id, CancellationToken cancellationToken = default);
}