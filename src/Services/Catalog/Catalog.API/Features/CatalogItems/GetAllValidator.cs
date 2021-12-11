namespace Catalog.API.Features.CatalogItems;

public class GetAllValidator : AbstractValidator<GetAll.Query>
{
    public GetAllValidator()
    {
        RuleFor(query => query).NotNull();
        RuleFor(query => query.PageIndex).GreaterThanOrEqualTo(default(int));
        RuleFor(query => query.PageSize).GreaterThanOrEqualTo(default(int));

        When(query => !string.IsNullOrWhiteSpace(query.Ids), () =>
        {
            Transform(query => query.Ids, StringToGuidList)
            .NotEmpty()
            .ForEach(id => id.NotEmpty()).When(query => !string.IsNullOrWhiteSpace(query.Ids));
        });
        // .When(query => string.IsNullOrWhiteSpace(query.Ids));
        // Transform(query => query.Ids, StringToGuidList)
        //     .ForEach(id => id.NotEmpty())
        //     .When(query => query.Ids is not null);
    }

    private static IEnumerable<Guid> StringToGuidList(string value)
    => value.ToStringList().Any(x => !Guid.TryParse(x, out _))
        ? Enumerable.Empty<Guid>()
        : value.ToGuidList();

    private static bool ValidGuidList(string value)
    {
        return !value.ToStringList().Any(x => !Guid.TryParse(x, out _))
        && !value.ToGuidList().Any(x => x == Guid.Empty);
    }
}