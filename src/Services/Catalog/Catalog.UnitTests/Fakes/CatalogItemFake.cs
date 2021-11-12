using Catalog.API.Dtos;
using Catalog.API.Models;

namespace Catalog.UnitTests.Fakes
{
    public static class CatalogItemFake
    {
        public static CatalogItemDto CreateCatalogItemDtoFake() => new()
        {
            Id = 13,
            Name = "name",
        };
        
        public static CatalogItem CreateCatalogItemFake() => new()
        {
            Id = 13,
            Name = "name",
        };
    }
}