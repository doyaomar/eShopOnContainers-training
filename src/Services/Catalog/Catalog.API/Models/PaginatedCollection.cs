namespace Catalog.API.Features.Models;

public class PaginatedCollection<T> : Pagination
{
    public IReadOnlyCollection<T> Items { get; set; }

    public long Count { get; set; }

    public PaginatedCollection(IReadOnlyCollection<T> items) => Items = items;
}