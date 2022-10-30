
namespace Catalog.API.Features.CatalogItems;

public static class GetById
{
    public record Query(Guid id) : IRequest<CatalogItemDto?>;

    public class Handler : IRequestHandler<Query, CatalogItemDto?>
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

        public async Task<CatalogItemDto?> Handle(Query query, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(query);
            var item = await _db.FindAsync(query.id, cancellationToken);

            return _mapper.Map<CatalogItemDto>(item);
        }
    }
}