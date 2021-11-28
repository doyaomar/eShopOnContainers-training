namespace Catalog.API.Features.CatalogItems;

class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Create.Command, CatalogItem>();
        CreateMap<Update.Command, CatalogItem>();
    }
}
