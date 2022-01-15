namespace Catalog.API.Extensions;

internal static class CatalogItemsExtensions
{
    private const char Separator = ';';

    internal static IEnumerable<Guid> ToGuidList(this string ids)
    => string.IsNullOrWhiteSpace(ids)
    ? Enumerable.Empty<Guid>()
    : ToStringArray(ids).Select(Guid.Parse);

    internal static string[] ToStringArray(this string ids)
    => ids?.Split(Separator) ?? Array.Empty<string>();

    internal static PaginatedCollection<CatalogItemDto> ToPaginatedDto(
        this IMapper mapper,
        (IReadOnlyCollection<CatalogItem> Items, long Count) paginatedItems,
        int pageIndex,
        int pageSize)
        => new PaginatedCollection<CatalogItemDto>(mapper.Map<IReadOnlyCollection<CatalogItemDto>>(paginatedItems.Items))
        {
            Count = paginatedItems.Count,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
}