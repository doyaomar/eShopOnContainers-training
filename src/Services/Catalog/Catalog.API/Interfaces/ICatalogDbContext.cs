namespace Catalog.API.Interfaces;

public interface ICatalogDbContext
{
    Task<Guid> InsertOneAsync(CatalogItem item, CancellationToken cancellationToken = default);
    Task<CatalogItem?> FindOneAndDeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<CatalogItem?> FindOneAndReplaceAsync(CatalogItem item, CancellationToken cancellationToken = default);
    Task<CatalogItem?> FindAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<CatalogItem>> FindAllAsync(IEnumerable<Guid> ids, int page, int size, CancellationToken cancellationToken = default);
}