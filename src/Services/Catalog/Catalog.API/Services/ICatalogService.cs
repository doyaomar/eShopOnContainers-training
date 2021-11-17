using Catalog.API.Models;

namespace Catalog.API.Services;

public interface ICatalogService
{
    Task<CatalogItem?> CreateProductAsync(CatalogItem item);
    Task<CatalogItem?> DeleteProductAsync(long id);
    Task<CatalogItem?> UpdateProductAsync(CatalogItem item);
    Task<CatalogItem?> GetProductAsync(long id);
}
