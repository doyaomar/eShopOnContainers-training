namespace Catalog.API.Features.CatalogItems;

public class GetAllValidator : AbstractValidator<GetAll.Query>
{
    public GetAllValidator()
    {
        RuleFor(query => query.PageIndex).GreaterThanOrEqualTo(default(int));
        RuleFor(query => query.PageSize).GreaterThanOrEqualTo(1);
        Transform(query => query.Ids, StringToGuidList)
            .NotEmpty().WithMessage("'{PropertyName}' must contain valid Guids separated by ';'.")
            .ForEach(id => id.NotEmpty())
            .When(query => !string.IsNullOrWhiteSpace(query.Ids));
    }

    private static IEnumerable<Guid> StringToGuidList(string value)
    => value.ToStringList().Any(x => !Guid.TryParse(x, out _))
        ? Enumerable.Empty<Guid>()
        : value.ToGuidList();
}