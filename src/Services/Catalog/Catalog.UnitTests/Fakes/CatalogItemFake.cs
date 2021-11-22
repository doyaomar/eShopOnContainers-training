using System;
using Catalog.API.Dtos;
using Catalog.API.Models;
using Catalog.API.Requests;

namespace Catalog.UnitTests.Fakes
{
    public static class CatalogItemFake
    {
        public static CatalogItemDto GetCatalogItemDtoFake(Guid id) => new()
        {
            Id = id,
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

        public static CatalogItem GetCatalogItemFake()
        {
            var item = new CatalogItem
            {
                Name = "name",
                CatalogBrand = new CatalogBrand
                {
                    Id = 1,
                    Name = "catalogBrandName"
                },
                CatalogType = new CatalogType
                {
                    Id = 1,
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
                Id = 1,
                Name = "catalogBrandName"
            },
            CatalogType = new CatalogTypeDto
            {
                Id = 1,
                Name = "catalogTypeName"
            }
        };

        public static UpdateProductRequest GetUpdateProductRequestFake(Guid id) => new()
        {
            Id = id,
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
    }
}