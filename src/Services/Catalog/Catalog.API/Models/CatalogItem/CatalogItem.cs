namespace Catalog.API.Models;

public class CatalogItem
{
    public Guid Id { get; private set; }

    public string Name { get; init; } = default!;

    public string Description { get; init; } = default!;

    public decimal Price { get; init; }

    public string PictureFileName { get; init; } = default!;

    public CatalogType? CatalogType { get; init; }

    public CatalogBrand? CatalogBrand { get; init; }

    public int AvailableStock { get; init; }

    public int RestockThreshold { get; init; }

    public int MaxStockThreshold { get; init; }

    public bool IsOnReorder { get; init; }

    public CatalogItem SetId(Guid id)
    {
        Id = id;

        return this;
    }
}
