namespace Catalog.API.Features.CatalogItems;

public class GetAllValidator : AbstractValidator<GetAll.Query>
{
    public GetAllValidator()
    {
        RuleFor(query => query).NotNull();
        RuleFor(query => query.PageIndex).GreaterThanOrEqualTo(default(int));
        RuleFor(query => query.PageSize).GreaterThanOrEqualTo(default(int));
        Transform(query => query.Ids, StringToGuidList)
            .NotEmpty()
            .ForEach(id => id.NotEmpty())
            .When(query => query.Ids is not null);
    }

    private static IEnumerable<Guid> StringToGuidList(string value)
    => value.ToStringList().Any(x => !Guid.TryParse(x, out _))
        ? Enumerable.Empty<Guid>()
        : value.ToGuidList();
}