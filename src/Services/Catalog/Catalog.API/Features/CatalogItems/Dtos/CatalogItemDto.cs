namespace Catalog.API.Features.CatalogItems.Dtos;

public class CatalogItemDto
{
    public Guid Id { get; set; }
    
    public string Name { get; init; } = default!;

    public string Description { get; init; } = default!;

    public decimal Price { get; init; }

    public string PictureUri { get; set; } = default!;

    public CatalogTypeDto CatalogType { get; init; } = default!;

    public CatalogBrandDto CatalogBrand { get; init; } = default!;

    public int AvailableStock { get; init; }

    public int RestockThreshold { get; init; }

    public int MaxStockThreshold { get; init; }

    public bool IsOnReorder { get; init; }
}
