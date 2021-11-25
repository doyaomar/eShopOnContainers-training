namespace Catalog.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CatalogItem Mappings
        CreateMap<CreateProductRequest, CatalogItem>();
        CreateMap<UpdateProductRequest, CatalogItem>();

        // CatalogItemDto Mappings
        CreateMap<CatalogItem, CatalogItemViewModel>();
        CreateMap<CatalogType, CatalogTypeDto>().ReverseMap();
        CreateMap<CatalogBrand, CatalogBrandDto>().ReverseMap();
    }
}
