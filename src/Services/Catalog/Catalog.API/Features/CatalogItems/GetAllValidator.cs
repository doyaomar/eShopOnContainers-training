namespace Catalog.API.Features.CatalogItems;

public class GetAllValidator : AbstractValidator<GetAll.Query>
{
    private const string IdsErrorMessage = "'{PropertyName}' must contain valid Guids separated by ';'.";
    private const string IdErrorMessage = "'{PropertyName}' must contain valid Guids separated by ';'.";

    public GetAllValidator()
    {
        RuleFor(query => query.PageIndex).GreaterThanOrEqualTo(default(int));
        RuleFor(query => query.PageSize).GreaterThan(default(int));
        Transform(query => query.Ids, StringToGuidList)
            .NotEmpty().WithMessage(IdsErrorMessage)
            .ForEach(id => id.NotEmpty().WithMessage(IdErrorMessage))
            .When(query => !string.IsNullOrWhiteSpace(query.Ids));
    }

    private static IEnumerable<Guid> StringToGuidList(string value)
    => value.ToStringArray().Any(x => !Guid.TryParse(x, out _))
        ? Enumerable.Empty<Guid>()
        : value.ToGuidList();
}