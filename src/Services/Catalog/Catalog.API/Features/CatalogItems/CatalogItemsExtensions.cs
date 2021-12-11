namespace Catalog.API.Features.CatalogItems;

public static class CatalogItemsExtensions
{
    private const char SEPARATOR = ';';

    internal static IEnumerable<Guid> ToGuidList(this string ids)
    => ToStringList(ids).Select(Guid.Parse);

    internal static IEnumerable<string> ToStringList(this string ids)
    => ids?.Split(SEPARATOR) ?? Enumerable.Empty<string>();
}