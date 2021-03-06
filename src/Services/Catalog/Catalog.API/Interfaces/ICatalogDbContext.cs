namespace Catalog.API.Interfaces;

public interface ICatalogDbContext
{
    Task<Guid> InsertOneAsync(CatalogItem item, CancellationToken cancellationToken = default);

    Task<CatalogItem?> FindOneAndDeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<CatalogItem?> FindOneAndReplaceAsync(CatalogItem item, CancellationToken cancellationToken = default);

    Task<CatalogItem?> FindAsync(Guid id, CancellationToken cancellationToken = default);

    Task<(IReadOnlyCollection<CatalogItem> Items, long Count)> FindAllAsync(
        IEnumerable<Guid> ids,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<(IReadOnlyCollection<CatalogItem> Items, long Count)> FindByTypeAndBrandAsync(
        Guid typeId,
        Guid? brandId,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<(IReadOnlyCollection<CatalogItem> Items, long Count)> FindByBrandAsync(
        Guid brandId,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<(IReadOnlyCollection<CatalogItem> Items, long Count)> FindByNameAsync(
        string name,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<CatalogType>> FindAllTypesAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<CatalogBrand>> FindAllBrandsAsync(CancellationToken cancellationToken = default);
}