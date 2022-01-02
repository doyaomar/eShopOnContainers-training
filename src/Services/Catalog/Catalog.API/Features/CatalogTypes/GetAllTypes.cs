namespace Catalog.API.Features.CatalogTypes;

public class GetAllTypes
{
    public record Query() : IRequest<IReadOnlyCollection<CatalogTypeDto>>;

    public class Handler : IRequestHandler<Query, IReadOnlyCollection<CatalogTypeDto>>
    {
        private readonly ICatalogDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ICatalogDbContext context, IMapper mapper)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IReadOnlyCollection<CatalogTypeDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            var types = await _db.FindAllTypesAsync(cancellationToken);

            return _mapper.Map<IReadOnlyCollection<CatalogTypeDto>>(types);
        }
    }
}
