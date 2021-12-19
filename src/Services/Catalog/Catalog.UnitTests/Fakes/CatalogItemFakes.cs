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

    internal static List<CatalogItemDto> GetCatalogItemDtosFake(Guid id1, Guid id2) => new()
    {
        CatalogItemFakes.GetCatalogItemDtoFake(id1),
        CatalogItemFakes.GetCatalogItemDtoFake(id2)
    };

    internal static IEnumerable<T> GetCatalogItemsFake<T>(List<Guid> ids)
    {
        if (typeof(T) == typeof(CatalogItemDto))
        {
            return (IEnumerable<T>)ids.Select(id => CatalogItemFakes.GetCatalogItemDtoFake(id));
        }

        return (IEnumerable<T>)ids.Select(id => CatalogItemFakes.GetCatalogItemFake(id));
    }

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

    internal static GetAll.Query GetGetAllQueryFake(Guid firstId, Guid secondId) => new()
    {
        Ids = $"{firstId};{secondId}",
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

    internal static PaginatedDto<T> GetPaginatedDtoFake<T>(List<T> items)
    => new PaginatedDto<T>(items) { Count = 2, PageIndex = 0, PageSize = 8 };
}
