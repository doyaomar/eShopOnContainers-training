namespace Catalog.API.Features.CatalogItems.Dtos;

public abstract class Pagination
{
    private const int DEFAULT_PAGE_SIZE = 10;
    public int PageIndex { get; set; } = default;
    public int PageSize { get; set; } = DEFAULT_PAGE_SIZE;
}