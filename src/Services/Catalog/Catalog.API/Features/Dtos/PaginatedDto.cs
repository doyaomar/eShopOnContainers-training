namespace Catalog.API.Features.Dtos;

public class PaginatedCollection<T> : Pagination
{
    public IReadOnlyCollection<T> Items { get; set; }

    public long Count { get; set; }

    public PaginatedCollection(IReadOnlyCollection<T> items)
    {
        Items = items;
    }
}