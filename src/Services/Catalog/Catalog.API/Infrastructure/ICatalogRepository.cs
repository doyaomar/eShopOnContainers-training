using Catalog.API.Models;

namespace Catalog.API.Infrastructure;

public interface ICatalogRepository
{
    Task<CatalogItem> CreateAsync(CatalogItem item);
    void Delete(CatalogItem item);
    Task<CatalogItem> GetAsync(long id, bool asNoTracking = false);
    void Update(CatalogItem item);
    Task SaveChangesAsync();
}
