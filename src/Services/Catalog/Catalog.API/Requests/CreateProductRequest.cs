using Catalog.API.Dtos;

namespace Catalog.API.Requests
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public decimal Price { get; set; }

        public string PictureFileName { get; set; } = default!;

        public CatalogTypeDto CatalogType { get; set; } = default!;

        public CatalogBrandDto CatalogBrand { get; set; } = default!;

        public int AvailableStock { get; set; }

        public int RestockThreshold { get; set; }

        public int MaxStockThreshold { get; set; }

        public bool IsOnReorder { get; set; }
    }
}