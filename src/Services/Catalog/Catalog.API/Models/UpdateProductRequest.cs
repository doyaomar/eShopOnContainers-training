namespace Catalog.API.Models;

public class UpdateProductRequest : CatalogItemDto
{
    public Guid Id { get; init; }
}
