namespace Catalog.API.Features.CatalogItems;

public class GetPictureValidator : AbstractValidator<GetPicture.Query>
{
    public GetPictureValidator() => RuleFor(query => query.id).NotEmpty();
}