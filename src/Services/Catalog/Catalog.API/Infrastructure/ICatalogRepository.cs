using Catalog.API.Models;

namespace Catalog.API.Infrastructure;

public interface ICatalogRepository
{
    Task<CatalogItem?> CreateAsync(CatalogItem item);
    Task<CatalogItem?> DeleteAsync(long id);
    Task<CatalogItem?> UpdateAsync(CatalogItem item);
    Task<CatalogItem?> GetAsync(long id, bool asNoTracking = false);
}
