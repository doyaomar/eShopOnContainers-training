namespace Catalog.API.Models;

public abstract class Pagination
{
    private const int DEFAULT_PAGE_SIZE = 10;

    [FromQuery]
    public int PageIndex { get; set; } = default;

    [FromQuery]
    public int PageSize { get; set; } = DEFAULT_PAGE_SIZE;
}