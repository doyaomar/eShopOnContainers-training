namespace Catalog.API.Models;

public class CatalogType
{
    public Guid Id { get; private set; }

    public string Name { get; init; } = default!;
}