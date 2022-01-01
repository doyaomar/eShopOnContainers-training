namespace Catalog.API.Models;

public abstract class Pagination
{
    private const int DefaultPageSize = 10;

    [FromQuery]
    public int PageIndex { get; set; } = default;

    [FromQuery]
    public int PageSize { get; set; } = DefaultPageSize;
}