namespace Catalog.API.Extensions;

public static class CatalogItemsExtensions
{
    private const char SEPARATOR = ';';

    internal static IEnumerable<Guid> ToGuidList(this string ids)
    => string.IsNullOrWhiteSpace(ids)
    ? Enumerable.Empty<Guid>()
    : ToStringList(ids).Select(Guid.Parse);

    internal static IEnumerable<string> ToStringList(this string ids)
    => ids?.Split(SEPARATOR) ?? Enumerable.Empty<string>();
}