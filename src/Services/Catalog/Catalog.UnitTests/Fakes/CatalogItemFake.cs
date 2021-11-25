namespace Catalog.UnitTests.Fakes;

public static class CatalogItemFake
{
    public static CatalogItemViewModel GetCatalogItemViewModelFake(Guid id) => new()
    {
        Id = id,
        Name = "name",
        CatalogBrand = new CatalogBrandDto
        {
            Id = Guid.NewGuid(),
            Name = "catalogBrandName"
        },
        CatalogType = new CatalogTypeDto
        {
            Id = Guid.NewGuid(),
            Name = "catalogTypeName"
        }
    };

    public static CatalogItem GetCatalogItemFake()
    {
        var item = new CatalogItem
        {
            Name = "name",
            CatalogBrand = new CatalogBrand
            {
                Name = "catalogBrandName"
            },
            CatalogType = new CatalogType
            {
                Name = "catalogTypeName"
            }
        };
        item.SetId(Guid.NewGuid());

        return item;
    }

    public static CreateProductRequest GetCreateProductRequestFake() => new()
    {
        Name = "name",
        CatalogBrand = new CatalogBrandDto
        {
            Name = "catalogBrandName"
        },
        CatalogType = new CatalogTypeDto
        {
            Name = "catalogTypeName"
        }
    };

    public static UpdateProductRequest GetUpdateProductRequestFake(Guid id) => new()
    {
        Id = id,
        Name = "name",
        CatalogBrand = new CatalogBrandDto
        {
            Id = Guid.NewGuid(),
            Name = "catalogBrandName"
        },
        CatalogType = new CatalogTypeDto
        {
            Id = Guid.NewGuid(),
            Name = "catalogTypeName"
        }
    };
}
