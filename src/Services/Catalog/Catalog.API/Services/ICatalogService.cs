using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Models;

namespace Catalog.API.Services
{
    public interface ICatalogService
    {
        Task<long> CreateProductAsync(CatalogItem item);
        Task DeleteProductAsync(CatalogItem item);
        Task<IEnumerable<CatalogItem>> GetAllProductsAsync(int pageSize, int pageIndex);
        Task<CatalogItem> GetProductAsync(long id);
        Task<IEnumerable<CatalogItem>> GetProductsByIdsAsync(IEnumerable<long> ids);
        Task UpdateProductAsync(CatalogItem item);
    }
}