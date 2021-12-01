namespace Catalog.API.Features.CatalogItems;

public class DeleteValidator : AbstractValidator<Delete.Command>
{
    public DeleteValidator() => RuleFor(command => command.id).NotEmpty();
}