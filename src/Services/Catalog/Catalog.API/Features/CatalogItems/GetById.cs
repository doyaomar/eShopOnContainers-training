
namespace Catalog.API.Features.CatalogItems;

public class GetById
{
    public record Query(Guid id) : IRequest<CatalogItemDto?>;

    public class Handler : IRequestHandler<Query, CatalogItemDto?>
    {
        private readonly ICatalogDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ICatalogDbContext context, IMapper mapper)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CatalogItemDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));
            var cursor = await _db.CatalogItems.FindAsync(x => x.Id == request.id, null, cancellationToken);
            var item = await cursor.FirstOrDefaultAsync();

            return _mapper.Map<CatalogItemDto>(item);
        }
    }
}