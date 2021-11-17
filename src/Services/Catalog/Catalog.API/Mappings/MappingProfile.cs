using AutoMapper;
using Catalog.API.Dtos;
using Catalog.API.Models;
using Catalog.API.Requests;

namespace Catalog.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CatalogItem Mappings
        CreateMap<CreateProductRequest, CatalogItem>();
        CreateMap<UpdateProductRequest, CatalogItem>();

        // CatalogItemDto Mappings
        CreateMap<CatalogItem, CatalogItemDto>();
        CreateMap<CatalogType, CatalogTypeDto>();
        CreateMap<CatalogBrand, CatalogBrandDto>();
    }
}
