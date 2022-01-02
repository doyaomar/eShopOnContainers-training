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

    public async Task<(IReadOnlyCollection<CatalogItem> Items, long Count)> FindAllAsync(
        IEnumerable<Guid> ids,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        Expression<Func<CatalogItem, bool>> filter = ids.Any() ? x => ids.Contains(x.Id) : _ => true;

        return await FindCatalogItemsAsync(pageIndex, pageSize, filter, cancellationToken);
    }

    public async Task<(IReadOnlyCollection<CatalogItem> Items, long Count)> FindByTypeAndBrandAsync(
        Guid typeId,
        Guid? brandId,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        Expression<Func<CatalogItem, bool>> filter = brandId is null
        ? x => x.CatalogType.Id == typeId
        : x => x.CatalogType.Id == typeId && x.CatalogBrand.Id == brandId;

        return await FindCatalogItemsAsync(pageIndex, pageSize, filter, cancellationToken);
    }

    public async Task<(IReadOnlyCollection<CatalogItem> Items, long Count)> FindByBrandAsync(
        Guid brandId,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default)
        => await FindCatalogItemsAsync(pageIndex, pageSize, x => x.CatalogBrand.Id == brandId, cancellationToken);

    public async Task<(IReadOnlyCollection<CatalogItem> Items, long Count)> FindByNameAsync(
        string name,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default)
        => await FindCatalogItemsAsync(pageIndex, pageSize, x => x.Name.StartsWith(name), cancellationToken);

    private async Task<(IReadOnlyCollection<CatalogItem> Items, long Count)> FindCatalogItemsAsync(
        int pageIndex,
        int pageSize,
        Expression<Func<CatalogItem, bool>> filterExpression,
        CancellationToken cancellationToken = default)
    {
        var items = CatalogItems.Find(filterExpression)
                                .SortBy(x => x.Name)
                                .Skip(pageIndex * pageSize)
                                .Limit(pageSize)
                                .ToListAsync(cancellationToken);
        var count = CatalogItems.CountDocumentsAsync(filterExpression, null, cancellationToken);

        return (await items, await count);
    }

    public async Task<IReadOnlyCollection<CatalogType>> FindAllTypesAsync(CancellationToken cancellationToken = default)
    => await CatalogTypes.Find(_ => true).ToListAsync(cancellationToken);
}
