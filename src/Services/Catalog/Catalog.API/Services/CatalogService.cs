namespace Catalog.API.Services;

public class CatalogService : ICatalogService
{
    private readonly ICatalogRepository _repository;

    private readonly IGuidService _guidProvider;

    public CatalogService(ICatalogRepository catalogRepository, IGuidService guidProvider)
    {
        _repository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
        _guidProvider = guidProvider ?? throw new ArgumentNullException(nameof(guidProvider));
    }

    public async Task<CatalogItem> CreateProductAsync(CatalogItem item)
    => await _repository.CreateAsync(item.SetId(_guidProvider.GetNewGuid()));

    public async Task<CatalogItem?> UpdateProductAsync(CatalogItem item) => await _repository.UpdateAsync(item);

    public async Task<CatalogItem?> DeleteProductAsync(Guid id) => await _repository.DeleteAsync(id);

    public async Task<CatalogItem?> GetProductAsync(Guid id) => await _repository.GetAsync(id);
}
