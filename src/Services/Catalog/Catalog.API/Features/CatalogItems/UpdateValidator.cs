namespace Catalog.API.Features.CatalogItems;

public class UpdateValidator : AbstractValidator<Update.Command>
{
    private const string PictureFileNameErrorMessage = "'{PropertyName}' must end with a valid picture file extension.";

    public UpdateValidator()
    {
        RuleFor(cmd => cmd.Id).NotEmpty();
        RuleFor(cmd => cmd.AvailableStock).GreaterThan(default(int));
        RuleFor(cmd => cmd.Description).NotEmpty();
        RuleFor(cmd => cmd.Name).NotEmpty();
        RuleFor(cmd => cmd.PictureFileName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Must(pic => pic.HasValidExtension())
            .WithMessage(PictureFileNameErrorMessage);
        RuleFor(cmd => cmd.Price).GreaterThan(default(decimal));
        RuleFor(cmd => cmd.CatalogBrand).NotNull();
        RuleFor(cmd => cmd.CatalogType).NotNull();
        RuleFor(cmd => cmd.CatalogBrand!.Id).NotEmpty().When(cmd => cmd.CatalogBrand is not null);
        RuleFor(cmd => cmd.CatalogBrand!.Name).NotEmpty().When(cmd => cmd.CatalogBrand is not null);
        RuleFor(cmd => cmd.CatalogType!.Id).NotEmpty().When(cmd => cmd.CatalogType is not null);
        RuleFor(cmd => cmd.CatalogType!.Name).NotEmpty().When(cmd => cmd.CatalogType is not null);
    }
}