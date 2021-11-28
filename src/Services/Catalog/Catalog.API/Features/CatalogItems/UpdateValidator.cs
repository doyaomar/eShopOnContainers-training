namespace Catalog.API.Features.CatalogItems;

public class UpdateValidator : AbstractValidator<Update.Command>
{
    public UpdateValidator()
    {
        RuleFor(command => command.Id).NotEqual(Guid.Empty);
        RuleFor(command => command.AvailableStock).GreaterThan(0);
        RuleFor(command => command.Description).NotEmpty();
        RuleFor(command => command.Name).NotNull().NotEmpty();
        RuleFor(command => command.Price).GreaterThan(0);
        RuleFor(command => command.CatalogBrand).NotNull();
        RuleFor(command => command.CatalogType).NotNull();
        RuleFor(command => command.CatalogBrand!.Id).NotEqual(Guid.Empty).When(command => command.CatalogBrand is not null);
        RuleFor(command => command.CatalogType!.Id).NotEqual(Guid.Empty).When(command => command.CatalogType is not null); ;
        RuleFor(command => command.CatalogBrand!.Name).NotNull().NotEmpty().When(command => command.CatalogBrand is not null); ;
        RuleFor(command => command.CatalogType!.Name).NotNull().NotEmpty().When(command => command.CatalogType is not null); ;
    }
}