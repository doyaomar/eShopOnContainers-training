namespace Catalog.API.Features.CatalogItems;

public class GetByIdValidator : AbstractValidator<GetById.Query>
{
    public GetByIdValidator() => RuleFor(command => command.id).NotEmpty();
}