namespace Catalog.UnitTests.Fakes;

internal static class CatalogTypeFakes
{
    internal static CatalogTypeDto GetCatalogTypeDtoFake(Guid id) => new CatalogTypeDto
    {
        Id = id,
        Name = "name"
    };

    internal static CatalogType GetCatalogTypeFake(Guid id)
    {
        var type = new CatalogType
        {
            Name = "name"
        };
        type.SetId(id);

        return type;
    }

    internal static IReadOnlyCollection<CatalogTypeDto> GetCatalogTypeDtosFake(List<Guid> ids)
    => ids.Select(GetCatalogTypeDtoFake).ToList();

    internal static IReadOnlyCollection<CatalogType> GetCatalogTypesFake(List<Guid> ids)
    => ids.Select(GetCatalogTypeFake).ToList();
}