using Catalog.API.Models;

namespace Catalog.API.Services;

public interface ICatalogService
{
    Task<CatalogItem> CreateProductAsync(CatalogItem item);
    Task DeleteProductAsync(CatalogItem item);
    Task<CatalogItem> GetProductAsync(long id, bool asNoTracking = false);
    Task UpdateProductAsync(CatalogItem item);
}
