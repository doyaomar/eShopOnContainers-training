namespace Catalog.UnitTests.Fakes;

public static class CatalogItemFakes
{
    public static Create.Command GetCreateCommandFake() => new()
    {
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

    public static Update.Command GetUpdateCommandFake(Guid id) => new()
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

    public static CatalogItemDto GetCatalogItemDtoFake(Guid id) => new()
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
