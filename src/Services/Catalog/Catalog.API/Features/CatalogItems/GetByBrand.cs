namespace Catalog.API.Features.CatalogItems;

public class GetByBrand
{
    public class Query : Pagination, IRequest<PaginatedDto<CatalogItemDto>>
    {
        public Guid CatalogBrandId { get; set; }
    }

    public class Handler : IRequestHandler<Query, PaginatedDto<CatalogItemDto>>
    {
        private readonly ICatalogDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ICatalogDbContext context, IMapper mapper)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PaginatedDto<CatalogItemDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            _ = query ?? throw new ArgumentNullException(nameof(query));
            (IReadOnlyCollection<CatalogItem> Items, long Count) paginatedItems = await _db.FindByBrandAsync(
                query.CatalogBrandId,
                query.PageIndex,
                query.PageSize,
                cancellationToken);

            return _mapper.ToPaginatedDto(
                paginatedItems,
                query.PageIndex,
                query.PageSize);
        }
    }
}