namespace Catalog.API.Features.CatalogItems;

public class GetByNameValidator : AbstractValidator<GetByName.Query>
{
    public GetByNameValidator()
    {
        RuleFor(query => query.PageIndex).GreaterThanOrEqualTo(default(int));
        RuleFor(query => query.PageSize).GreaterThan(default(int));
        RuleFor(query => query.Name).NotEmpty();
    }
}