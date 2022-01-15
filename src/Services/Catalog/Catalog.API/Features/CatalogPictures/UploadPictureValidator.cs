namespace Catalog.API.Features.CatalogPictures;

public class UploadPictureValidator : AbstractValidator<UploadPicture.Command>
{
    private const int Mb = 1000000;
    private readonly CatalogSettings _catalogSettings;

    public UploadPictureValidator(IOptions<CatalogSettings> settings)
    {
        _catalogSettings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));

        RuleFor(command => command.Id).NotEmpty();
        RuleFor(Command => Command.PictureFile)
        .NotNull()
        .Must(pic => pic.HasValidSizeLimit(_catalogSettings.CatalogItemPictureSizeLimit))
        .WithMessage($"'{{PropertyName}}' size must not exceed {_catalogSettings.CatalogItemPictureSizeLimit / Mb} mb.")
        .Must(pic => pic.HasValidExtension() && pic.HasValidSignature())
        .WithMessage("'{PropertyName}' must have a valid extension and signature.");
    }
}