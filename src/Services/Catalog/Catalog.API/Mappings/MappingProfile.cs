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
            CreateMap<CatalogItem, CreateProductRequest>().ReverseMap();
            CreateMap<CatalogItem, UpdateProductRequest>().ReverseMap();
            CreateMap<CatalogItem, CatalogItemDto>().ReverseMap();
        }
    }
}