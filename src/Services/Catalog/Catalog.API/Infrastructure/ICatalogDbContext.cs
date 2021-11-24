namespace Catalog.API.Infrastructure;

public interface ICatalogDbContext
{
    Task<CatalogItem?> CreateAsync(CatalogItem item);

    Task<CatalogItem?> DeleteAsync(Guid id);

    Task<CatalogItem?> UpdateAsync(CatalogItem item);

    Task<CatalogItem?> GetAsync(Guid id);
}
