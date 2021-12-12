namespace Catalog.API.Features.CatalogItems;

public class GetAll
{
    public class Query : Pagination, IRequest<PaginatedDto<CatalogItemDto>>
    {
        /// <summary>
        /// string of guids sepratated by ';'
        /// </summary>
        public string Ids { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<GetAll.Query, PaginatedDto<CatalogItemDto>>
    {
        private readonly ICatalogDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ICatalogDbContext context, IMapper mapper)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PaginatedDto<CatalogItemDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));
            IEnumerable<Guid> ids = request.Ids.ToGuidList();
            (IReadOnlyCollection<CatalogItem> Items, long Count) paginatedItems = await _db.FindAllAsync(ids, request.PageIndex, request.PageSize, cancellationToken);

            return new PaginatedDto<CatalogItemDto>(_mapper.Map<IReadOnlyCollection<CatalogItemDto>>(paginatedItems.Items),
                                                    paginatedItems.Count,
                                                    request.PageIndex,
                                                    request.PageSize);
        }
    }
}
