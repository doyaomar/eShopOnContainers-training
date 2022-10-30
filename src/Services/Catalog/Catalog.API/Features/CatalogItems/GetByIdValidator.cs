namespace Catalog.API.Features.CatalogItems;

public class GetByIdValidator : AbstractValidator<GetById.Query>
{
    public GetByIdValidator() => RuleFor(query => query.id).NotEmpty();
}