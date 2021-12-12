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

    public static List<CatalogItemDto> GetCatalogItemDtosFake(Guid id1, Guid id2) => new()
    {
        CatalogItemFakes.GetCatalogItemDtoFake(id1),
        CatalogItemFakes.GetCatalogItemDtoFake(id2)
    };

    public static CatalogItem GetCatalogItemFake(Guid? id = null)
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

        if (id is not null)
            item.SetId(Guid.NewGuid());

        return item;
    }

    public static GetAll.Query GetGetAllQueryFake(Guid firstIdStub, Guid secondIdStub) => new()
    {
        Ids = $"{firstIdStub};{secondIdStub}",
        PageIndex = 0,
        PageSize = 8
    };

    public static PaginatedDto<T> GetPaginatedDtoFake<T>(List<T> items)
    => new PaginatedDto<T>(items, 2, 0, 8);
}
