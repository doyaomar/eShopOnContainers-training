namespace Catalog.API.Features.CatalogBrands;

class MappingProfile : Profile
{
    public MappingProfile() => CreateMap<CatalogBrand, CatalogBrandDto>().ReverseMap();
}
