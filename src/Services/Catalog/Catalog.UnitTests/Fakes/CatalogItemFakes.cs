namespace Catalog.UnitTests.Fakes;

internal static class CatalogItemFakes
{
    internal static Create.Command GetCreateCommandFake() => new()
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

    internal static Update.Command GetUpdateCommandFake(Guid id) => new()
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

    internal static CatalogItemDto GetCatalogItemDtoFake(Guid id) => new()
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

    internal static List<CatalogItem> GetCatalogItemsFake(List<Guid> ids)
    => ids.Select(id => CatalogItemFakes.GetCatalogItemFake(id)).ToList();

    internal static List<CatalogItemDto> GetCatalogItemDtosFake(List<Guid> ids)
    => ids.Select(id => CatalogItemFakes.GetCatalogItemDtoFake(id)).ToList();

    internal static CatalogItem GetCatalogItemFake(Guid? id = null)
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

    internal static GetAll.Query GetGetAllQueryFake(string ids) => new()
    {
        Ids = ids,
        PageIndex = 0,
        PageSize = 8
    };

    internal static GetByTypeAndBrand.Query GetByTypeAndBrandFake(Guid typeId, Guid brandId) => new()
    {
        CatalogTypeId = typeId,
        CatalogBrandId = brandId,
        PageIndex = 0,
        PageSize = 8
    };

    internal static PaginatedDto<T> GetPaginatedDtoFake<T>(IReadOnlyCollection<T> items)
    => new PaginatedDto<T>(items) { Count = 2, PageIndex = 0, PageSize = 8 };
}
