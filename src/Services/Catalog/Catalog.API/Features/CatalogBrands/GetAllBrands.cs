namespace Catalog.API.Features.CatalogBrands;

public class GetAllBrands
{
    public record Query() : IRequest<IReadOnlyCollection<CatalogBrandDto>>;

    public class Handler : IRequestHandler<Query, IReadOnlyCollection<CatalogBrandDto>>
    {
        private readonly ICatalogDbContext _db;

        private readonly IMapper _mapper;

        public Handler(ICatalogDbContext context, IMapper mapper)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IReadOnlyCollection<CatalogBrandDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            var brands = await _db.FindAllBrandsAsync(cancellationToken);

            return _mapper.Map<IReadOnlyCollection<CatalogBrandDto>>(brands);
        }
    }
}
