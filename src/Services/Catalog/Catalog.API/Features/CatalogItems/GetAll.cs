namespace Catalog.API.Features.CatalogItems;

public class GetAll
{
    public class Query : IPagination, IRequest<IReadOnlyCollection<CatalogItemDto>>
    {
        private const int DEFAULT_PAGE_SIZE = 10;

        /// <summary>
        /// string of guids sepratated by ';'
        /// </summary>
        public string Ids { get; set; } = default!;
        public int PageIndex { get; set; } = default;
        public int PageSize { get; set; } = DEFAULT_PAGE_SIZE;
    }

    public class Handler : IRequestHandler<GetAll.Query, IReadOnlyCollection<CatalogItemDto>>
    {
        private readonly ICatalogDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ICatalogDbContext context, IMapper mapper)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IReadOnlyCollection<CatalogItemDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            IEnumerable<Guid> ids = request.Ids.ToGuidList();

            var items = await _db.FindAllAsync(ids, request.PageIndex, request.PageSize, cancellationToken);

            throw new NotImplementedException();
        }
    }
}
