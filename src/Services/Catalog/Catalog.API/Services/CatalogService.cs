using Catalog.API.Infrastructure;
using Catalog.API.Models;

namespace Catalog.API.Services;

public class CatalogService : ICatalogService
{
    private readonly ICatalogDbContext _catalogDbContext;

    public CatalogService(ICatalogDbContext catalogDbContext)
    {
        _catalogDbContext = catalogDbContext ?? throw new ArgumentNullException(nameof(catalogDbContext));
    }

    public async Task<CatalogItem?> CreateProductAsync(CatalogItem item)
    {
        item.SetId(Guid.NewGuid());

        return await _catalogDbContext.CreateAsync(item);
    }

    public async Task<CatalogItem?> UpdateProductAsync(CatalogItem item) => await _catalogDbContext.UpdateAsync(item);


    public async Task<CatalogItem?> DeleteProductAsync(Guid id) => await _catalogDbContext.DeleteAsync(id);

    public async Task<CatalogItem?> GetProductAsync(Guid id) => await _catalogDbContext.GetAsync(id);
}
