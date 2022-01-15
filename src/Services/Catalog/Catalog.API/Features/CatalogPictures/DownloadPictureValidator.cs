namespace Catalog.API.Features.CatalogPictures;

public class DownloadPictureValidator : AbstractValidator<DownloadPicture.Query>
{
    public DownloadPictureValidator() => RuleFor(query => query.id).NotEmpty();
}