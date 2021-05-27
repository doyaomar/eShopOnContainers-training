using Catalog.API.Models;

namespace Catalog.API.Dtos
{
    public class CatalogItemDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureFileName { get; set; }

        public string PictureUri { get; set; }

        public CatalogType CatalogType { get; set; }

        public CatalogBrand CatalogBrand { get; set; }
    }
}