namespace Catalog.API.Features.CatalogItems;

class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Create.Command, CatalogItem>();
        CreateMap<Update.Command, CatalogItem>();
        CreateMap<CatalogItem, CatalogItemDto>()
        .ForMember(dest => dest.PictureUri, opt => opt.MapFrom<PictureUriResolver>());
    }
}
