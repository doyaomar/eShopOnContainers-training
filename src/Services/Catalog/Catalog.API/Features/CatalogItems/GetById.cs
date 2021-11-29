
namespace Catalog.API.Features.CatalogItems;

public class GetById
{
    public record Query(Guid id) : IRequest<CatalogItemDto?>;

    public class Handler : IRequestHandler<Query, CatalogItemDto?>
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;

        public Handler(CatalogDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CatalogItemDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return null; // replace by exception ??
            }

            var cursor = await _context.CatalogItems.FindAsync(x => x.Id == request.id, null, cancellationToken);
            var item = await cursor.FirstOrDefaultAsync();

            return _mapper.Map<CatalogItemDto>(item);
        }
    }
}