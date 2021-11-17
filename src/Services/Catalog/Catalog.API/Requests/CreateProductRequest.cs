using Catalog.API.Models;

namespace Catalog.API.Requests
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public decimal Price { get; set; }

        public string PictureFileName { get; set; } = default!;        

        public int CatalogTypeId { get; set; }

        public int CatalogBrandId { get; set; }

        public int AvailableStock { get; set; }

        public int RestockThreshold { get; set; }

        public int MaxStockThreshold { get; set; }

        public bool IsOnReorder { get; set; }
    }
}