namespace Catalog.API.Infrastructure.Settings;

public class CatalogDbSettings
{
    public string ConnectionString { get; set; } = default!;

    public string DatabaseName { get; set; } = default!;

    public string CatalogItemsCollectionName { get; set; } = default!;

    public string CatalogBrandsCollectionName { get; set; } = default!;

    public string CatalogTypesCollectionName { get; set; } = default!;
}