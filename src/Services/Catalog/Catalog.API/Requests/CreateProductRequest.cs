using Catalog.API.Models;

namespace Catalog.API.Requests
{
    public class CreateProductRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureFileName { get; set; }

        public string PictureUri { get; set; }

        public int CatalogTypeId { get; set; }

        public int CatalogBrandId { get; set; }
    }
}