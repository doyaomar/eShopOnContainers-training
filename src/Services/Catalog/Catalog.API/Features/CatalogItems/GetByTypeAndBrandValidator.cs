namespace Catalog.API.Features.CatalogItems;

public class GetByTypeAndBrandValidator : AbstractValidator<GetByTypeAndBrand.Query>
{
    public GetByTypeAndBrandValidator()
    {
        RuleFor(query => query.PageIndex).GreaterThanOrEqualTo(default(int));
        RuleFor(query => query.PageSize).GreaterThan(default(int));
        RuleFor(query => query.CatalogTypeId).NotEmpty();
        RuleFor(query => query.CatalogBrandId).NotEqual(Guid.Empty).When(query => query.CatalogBrandId.HasValue);
    }
}