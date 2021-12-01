namespace Catalog.API.Models;

public class CatalogBrand : Entity<Guid>
{
    public string Name { get; init; } = default!;
}
