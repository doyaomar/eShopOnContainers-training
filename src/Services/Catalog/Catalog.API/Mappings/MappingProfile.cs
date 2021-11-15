using AutoMapper;
using Catalog.API.Dtos;
using Catalog.API.Models;
using Catalog.API.Requests;

namespace Catalog.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CatalogItem Mappings
            CreateMap<CreateProductRequest, CatalogItem>().ReverseMap();
            CreateMap<UpdateProductRequest, CatalogItem>().ReverseMap();
            CreateMap<CatalogItem, CatalogItemDto>().ReverseMap();
        }
    }
}