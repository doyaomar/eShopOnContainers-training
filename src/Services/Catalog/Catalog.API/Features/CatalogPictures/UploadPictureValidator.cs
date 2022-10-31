namespace Catalog.API.Features.CatalogPictures;

public class UploadPictureValidator : AbstractValidator<UploadPicture.Command>
{
    private const int Mb = 1000000;
    private const string ValidExtensionErrorMessage = "'{PropertyName}' must have a valid extension and signature.";
    private const string ValidSizeLimitErrorMessage = "'{{PropertyName}}' size must not exceed {0} mb.";
    private readonly CatalogSettings _catalogSettings;

    public UploadPictureValidator(IOptions<CatalogSettings> settings)
    {
        _catalogSettings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));

        RuleFor(command => command.Id).NotEmpty();
        RuleFor(Command => Command.PictureFile)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Must(pic => pic.HasValidSizeLimit(_catalogSettings.CatalogItemPictureSizeLimit))
            .WithMessage(string.Format(ValidSizeLimitErrorMessage, _catalogSettings.CatalogItemPictureSizeLimit / Mb))
            .Must(pic => pic.HasValidExtension() && pic.HasValidSignature())
            .WithMessage(ValidExtensionErrorMessage);
    }
}