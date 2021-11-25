namespace Catalog.API.Dtos;

public class CatalogItemDto
{
    public string Name { get; init; } = default!;

    public string Description { get; init; } = default!;

    public decimal Price { get; init; }

    public string PictureFileName { get; init; } = default!;

    public CatalogTypeDto CatalogType { get; init; } = default!;

    public CatalogBrandDto CatalogBrand { get; init; } = default!;

    public int AvailableStock { get; init; }

    public int RestockThreshold { get; init; }

    public int MaxStockThreshold { get; init; }

    public bool IsOnReorder { get; init; }
}
