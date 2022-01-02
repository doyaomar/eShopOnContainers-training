namespace Catalog.API.Features.CatalogTypes;

class MappingProfile : Profile
{
    public MappingProfile() => CreateMap<CatalogType, CatalogTypeDto>().ReverseMap();
}
