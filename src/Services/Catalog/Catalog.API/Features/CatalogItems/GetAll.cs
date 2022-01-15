namespace Catalog.API.Features.CatalogItems;

public class GetAll
{
    public class Query : Pagination, IRequest<PaginatedCollection<CatalogItemDto>>
    {
        /// <summary>
        /// string of guids sepratated by ';'
        /// </summary>
        public string Ids { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<GetAll.Query, PaginatedCollection<CatalogItemDto>>
    {
        private readonly ICatalogDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ICatalogDbContext context, IMapper mapper)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PaginatedCollection<CatalogItemDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            _ = query ?? throw new ArgumentNullException(nameof(query));
            IEnumerable<Guid> ids = query.Ids.ToGuidList();
            (IReadOnlyCollection<CatalogItem> Items, long Count) paginatedItems = await _db.FindAllAsync(
                ids,
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
