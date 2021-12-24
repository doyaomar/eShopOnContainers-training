namespace Catalog.API.Features.CatalogItems;

public class GetByBrandValidator : AbstractValidator<GetByBrand.Query>
{
    public GetByBrandValidator()
    {
        RuleFor(query => query.PageIndex).GreaterThanOrEqualTo(default(int));
        RuleFor(query => query.PageSize).GreaterThan(default(int));
        RuleFor(query => query.CatalogBrandId).NotEmpty();
    }
}