namespace Catalog.API.Features.CatalogItems;

public class GetByName
{
    public class Query : Pagination, IRequest<PaginatedCollection<CatalogItemDto>>
    {
        public string Name { get; set; } = default!;
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
            (IReadOnlyCollection<CatalogItem> Items, long Count) paginatedItems = await _db.FindByNameAsync(
                query.Name,
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