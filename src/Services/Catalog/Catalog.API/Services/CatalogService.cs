using Catalog.API.Infrastructure;
using Catalog.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogRepository _catalogRepository;

        public CatalogService(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
        }

        public async Task<CatalogItem> GetProductAsync(long id)
        {
            return await _catalogRepository.GetAsync(id);
        }

        public async Task<IEnumerable<CatalogItem>> GetAllProductsAsync(int pageSize, int pageIndex)
        {
            return await _catalogRepository.GetAllAsync(pageSize, pageIndex);
        }

        public async Task<IEnumerable<CatalogItem>> GetProductsByIdsAsync(IEnumerable<long> ids)
        {
            return await _catalogRepository.GetByIdsAsync(ids);
        }

        public async Task<long> CreateProductAsync(CatalogItem item)
        {
            long createdItemId = await _catalogRepository.CreateAsync(item);
            await _catalogRepository.SaveChangesAsync();

            return createdItemId;
        }

        public async Task UpdateProductAsync(CatalogItem item)
        {
            _catalogRepository.Update(item);
            await _catalogRepository.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(CatalogItem item)
        {
            _catalogRepository.Delete(item);
            await _catalogRepository.SaveChangesAsync();
        }
    }
}