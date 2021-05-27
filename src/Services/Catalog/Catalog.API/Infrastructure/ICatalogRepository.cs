using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Models;

namespace Catalog.API.Infrastructure
{
    public interface ICatalogRepository
    {
        Task<long> CreateAsync(CatalogItem item);
        void Delete(CatalogItem item);
        Task<IEnumerable<CatalogItem>> GetAllAsync(int pageSize, int pageIndex);
        Task<CatalogItem> GetAsync(long id);
        Task<IEnumerable<CatalogItem>> GetByIdsAsync(IEnumerable<long> ids);
        Task<long> GetCountAsync();
        Task SaveChangesAsync();
        void Update(CatalogItem item);
    }
}