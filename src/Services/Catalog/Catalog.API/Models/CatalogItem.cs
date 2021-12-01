namespace Catalog.API.Models;

public class CatalogItem : Entity<Guid>
{
    public string Name { get; init; } = default!;

    public string Description { get; init; } = default!;

    public decimal Price { get; init; }

    public string PictureFileName { get; init; } = default!;

    public CatalogType CatalogType { get; init; } = default!;

    public CatalogBrand CatalogBrand { get; init; } = default!;

    public int AvailableStock { get; init; }

    public int RestockThreshold { get; init; }

    public int MaxStockThreshold { get; init; }

    public bool IsOnReorder { get; init; }
}
