namespace Catalog.API.Features.CatalogItems;

public class GetAllValidator : AbstractValidator<GetAll.Query>
{
    public GetAllValidator()
    {
        RuleFor(query => query.PageIndex).GreaterThanOrEqualTo(default(int));
        RuleFor(query => query.PageSize).GreaterThan(default(int));
        Transform(query => query.Ids, StringToGuidList)
            .NotEmpty().WithMessage("'{PropertyName}' must contain valid Guids separated by ';'.")
            .ForEach(id => id.NotEmpty().WithMessage("'{PropertyName}' must contain valid Guids separated by ';'."))
            .When(query => !string.IsNullOrWhiteSpace(query.Ids));
    }

    private static IEnumerable<Guid> StringToGuidList(string value)
    => value.ToStringArray().Any(x => !Guid.TryParse(x, out _))
        ? Enumerable.Empty<Guid>()
        : value.ToGuidList();
}