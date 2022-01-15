namespace Catalog.API.Features.CatalogItems;

public class PictureUriResolver : IValueResolver<CatalogItem, CatalogItemDto, string>
{
    private readonly CatalogSettings _catalogSettings;

    public PictureUriResolver(IOptions<CatalogSettings> settings)
    => _catalogSettings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));

    public string Resolve(CatalogItem source, CatalogItemDto destination, string destMember, ResolutionContext context)
    => string.Format(_catalogSettings.CatalogItemPictureBaseUrl, destination.Id);
}