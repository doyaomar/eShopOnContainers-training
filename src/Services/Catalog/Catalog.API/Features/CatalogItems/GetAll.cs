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

    public class Handler : IRequestHandler<Query, PaginatedCollection<CatalogItemDto>>
    {
        private readonly ICatalogDbContext _db;
        private readonly IMapper _mapper;
        private readonly IValidator<Query> _validator;

        public Handler(ICatalogDbContext context, IMapper mapper, IValidator<Query> validator)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<PaginatedCollection<CatalogItemDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(query);
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
