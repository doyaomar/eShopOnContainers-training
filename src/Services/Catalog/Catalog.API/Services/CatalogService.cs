using Catalog.API.Infrastructure;
using Catalog.API.Models;

namespace Catalog.API.Services;

public class CatalogService : ICatalogService
{
    private readonly ICatalogRepository _catalogRepository;

    public CatalogService(ICatalogRepository catalogRepository)
    {
        _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
    }

    public async Task<CatalogItem?> CreateProductAsync(CatalogItem item)
    {
        return await _catalogRepository.CreateAsync(item);
    }

    public async Task<CatalogItem?> UpdateProductAsync(CatalogItem item)
    {
        return await _catalogRepository.UpdateAsync(item);
    }


    public async Task<CatalogItem?> DeleteProductAsync(long id)
    {
        return await _catalogRepository.DeleteAsync(id);
    }

    public async Task<CatalogItem?> GetProductAsync(long id, bool asNoTracking = false)
    {
        return await _catalogRepository.GetAsync(id, asNoTracking);
    }
}
