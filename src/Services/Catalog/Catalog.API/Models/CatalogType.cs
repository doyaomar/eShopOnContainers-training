namespace Catalog.API.Models;

public class CatalogType : Entity<Guid>
{
    public string Name { get; init; } = default!;
}