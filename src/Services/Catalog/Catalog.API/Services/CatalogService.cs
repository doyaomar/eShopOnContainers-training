namespace Catalog.API.Services;

public class CatalogService : ICatalogService
{
    private readonly ICatalogDbContext _catalogDbContext;

    private readonly IGuidProvider _guidProvider;

    public CatalogService(ICatalogDbContext catalogDbContext, IGuidProvider guidProvider)
    {
        _catalogDbContext = catalogDbContext ?? throw new ArgumentNullException(nameof(catalogDbContext));
        _guidProvider = guidProvider ?? throw new ArgumentNullException(nameof(guidProvider));
    }

    public async Task<CatalogItem?> CreateProductAsync(CatalogItem item)
    {
        item.SetId(_guidProvider.GetNewGuid());

        return await _catalogDbContext.CreateAsync(item);
    }

    public async Task<CatalogItem?> UpdateProductAsync(CatalogItem item) => await _catalogDbContext.UpdateAsync(item);

    public async Task<CatalogItem?> DeleteProductAsync(Guid id) => await _catalogDbContext.DeleteAsync(id);

    public async Task<CatalogItem?> GetProductAsync(Guid id) => await _catalogDbContext.GetAsync(id);
}
