using Catalog.API.Models;

namespace Catalog.API.Services;

public interface ICatalogService
{
    Task<CatalogItem?> CreateProductAsync(CatalogItem item);

    Task<CatalogItem?> DeleteProductAsync(Guid id);
    
    Task<CatalogItem?> UpdateProductAsync(CatalogItem item);
    
    Task<CatalogItem?> GetProductAsync(Guid id);
}
