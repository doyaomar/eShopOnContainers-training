namespace Catalog.API.Infrastructure;

public class CatalogDbSettings
{
    public string DatabaseName { get; set; } = default!;

    public string CatalogItemsCollectionName { get; set; } = default!;

    public string CatalogBrandsCollectionName { get; set; } = default!;

    public string CatalogTypesCollectionName { get; set; } = default!;
}