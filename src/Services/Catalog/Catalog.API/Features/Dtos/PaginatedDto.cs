namespace Catalog.API.Features.Dtos;

public class PaginatedDto<T> : Pagination
{
    public IReadOnlyCollection<T> Items { get; set; }

    public long Count { get; set; }

    public PaginatedDto(IReadOnlyCollection<T> items)
    {
        Items = items;
    }
}