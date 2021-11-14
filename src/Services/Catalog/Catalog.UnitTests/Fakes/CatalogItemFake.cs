using Catalog.API.Dtos;
using Catalog.API.Models;
using Catalog.API.Requests;

namespace Catalog.UnitTests.Fakes
{
    public static class CatalogItemFake
    {
        public static CatalogItemDto GetCatalogItemDtoFake() => new()
        {
            Id = 1,
            Name = "name",
            CatalogBrandId = 1,
            CatalogTypeId = 1
        };

        public static CatalogItem GetCatalogItemFake() => new()
        {
            Id = 1,
            Name = "name",
            CatalogBrandId = 1,
            CatalogTypeId = 1
        };

        public static CreateProductRequest GetCreateProductRequestFake() => new()
        {
            Name = "name",
            CatalogBrandId = 1,
            CatalogTypeId = 1
        };

        public static UpdateProductRequest GetUpdateProductRequestFake() => new()
        {
            Name = "name",
            CatalogBrandId = 1,
            CatalogTypeId = 1
        };
    }
}