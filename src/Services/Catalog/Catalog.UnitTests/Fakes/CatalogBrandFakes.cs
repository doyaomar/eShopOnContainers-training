namespace Catalog.UnitTests.Fakes;

internal static class CatalogBrandFakes
{
    internal static CatalogBrandDto GetCatalogBrandDtoFake(Guid id) => new CatalogBrandDto
    {
        Id = id,
        Name = "name"
    };

    internal static CatalogBrand GetCatalogBrandFake(Guid id)
    {
        var brand = new CatalogBrand
        {
            Name = "name"
        };
        brand.SetId(id);

        return brand;
    }

    internal static IReadOnlyCollection<CatalogBrandDto> GetCatalogBrandDtosFake(List<Guid> ids)
    => ids.Select(GetCatalogBrandDtoFake).ToList();

    internal static IReadOnlyCollection<CatalogBrand> GetCatalogBrandsFake(List<Guid> ids)
    => ids.Select(GetCatalogBrandFake).ToList();
}