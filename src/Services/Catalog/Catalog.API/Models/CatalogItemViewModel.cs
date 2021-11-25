namespace Catalog.API.Models;

public class CatalogItemViewModel : CatalogItemDto
{
    public Guid Id { get; init; }

    public string PictureUri { get; init; } = default!;
}
