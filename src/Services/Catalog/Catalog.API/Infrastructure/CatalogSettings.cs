namespace Catalog.API.Infrastructure;

public class CatalogSettings
{
    public string CatalogItemPictureBaseUrl { get; set; } = default!;

    public long CatalogItemPictureSizeLimit { get; set; } = default!;
}