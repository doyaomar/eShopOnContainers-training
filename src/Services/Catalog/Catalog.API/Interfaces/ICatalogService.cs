namespace Catalog.API.Interfaces;

public interface ICatalogService
{
    Task<CatalogItem> CreateProductAsync(CatalogItem item);

    Task<CatalogItem?> DeleteProductAsync(Guid id);

    Task<CatalogItem?> UpdateProductAsync(CatalogItem item);

    Task<CatalogItem?> GetProductAsync(Guid id);
}
