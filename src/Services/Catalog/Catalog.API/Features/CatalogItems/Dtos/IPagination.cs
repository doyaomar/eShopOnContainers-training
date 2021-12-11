namespace Catalog.API.Features.CatalogItems.Dtos;

interface IPagination
{
    int PageIndex { get; set; }
    int PageSize { get; set; }
}