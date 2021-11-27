namespace Catalog.API.Models;

public class CatalogType : IEntity
{
    public Guid Id { get; private set; }

    public string Name { get; init; } = default!;
}