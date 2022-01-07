namespace Catalog.API.Extensions;

public static class CatalogItemsExtensions
{
    private const char Separator = ';';

    internal static IEnumerable<Guid> ToGuidList(this string ids)
    => string.IsNullOrWhiteSpace(ids)
    ? Enumerable.Empty<Guid>()
    : ToStringList(ids).Select(Guid.Parse);

    internal static IEnumerable<string> ToStringList(this string ids)
    => ids?.Split(Separator) ?? Enumerable.Empty<string>();

    internal static PaginatedDto<CatalogItemDto> ToPaginatedDto(
        this IMapper mapper,
        (IReadOnlyCollection<CatalogItem> Items, long Count) paginatedItems,
        int pageIndex,
        int pageSize) => new PaginatedDto<CatalogItemDto>(mapper.Map<IReadOnlyCollection<CatalogItemDto>>(paginatedItems.Items))
        {
            Count = paginatedItems.Count,
            PageIndex = pageIndex,
            PageSize = pageSize
        };

    internal static bool HasValidExtension(this string pictureFileName)
    {
        string[] permittedExtensions = { ".png", ".gif", ".jpg", ".jpeg", ".bmp", ".tiff", ".wmf", ".jp2", ".svg" };
        var ext = Path.GetExtension(pictureFileName).ToLowerInvariant();

        return permittedExtensions.Contains(ext);
    }
}