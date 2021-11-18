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
            CatalogBrand = new CatalogBrandDto
            {
                Id = 1,
                Name = "catalogBrandName"
            },
            CatalogType = new CatalogTypeDto
            {
                Id = 1,
                Name = "catalogTypeName"
            }
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
            Id = 1,
            Name = "name",
            CatalogBrandId = 1,
            CatalogTypeId = 1
        };
    }
}