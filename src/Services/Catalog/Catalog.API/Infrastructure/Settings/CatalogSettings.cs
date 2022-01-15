namespace Catalog.API.Infrastructure.Settings;

public class CatalogSettings
{
    public string CatalogItemPictureBaseUrl { get; set; } = default!;

    public long CatalogItemPictureSizeLimit { get; set; } = default!;

    public string WebRootImagesPath { get; set; } = default!;
}