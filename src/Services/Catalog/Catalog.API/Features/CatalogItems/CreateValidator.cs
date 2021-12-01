namespace Catalog.API.Features.CatalogItems;

public class CreateValidator : AbstractValidator<Create.Command>
{
    public CreateValidator()
    {
        RuleFor(command => command).NotNull();
        RuleFor(command => command.AvailableStock).GreaterThan(default(int));
        RuleFor(command => command.Description).NotNull().NotEmpty();
        RuleFor(command => command.Name).NotNull().NotEmpty();
        RuleFor(command => command.Price).GreaterThan(default(decimal));
        RuleFor(command => command.CatalogBrand).NotNull();
        RuleFor(command => command.CatalogType).NotNull();
        RuleFor(command => command.CatalogBrand!.Id).NotEmpty().When(command => command.CatalogBrand is not null);
        RuleFor(command => command.CatalogType!.Id).NotEmpty().When(command => command.CatalogType is not null);
        RuleFor(command => command.CatalogBrand!.Name).NotNull().NotEmpty().When(command => command.CatalogBrand is not null);
        RuleFor(command => command.CatalogType!.Name).NotNull().NotEmpty().When(command => command.CatalogType is not null);
    }
}