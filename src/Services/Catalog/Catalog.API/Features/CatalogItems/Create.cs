namespace Catalog.API.Features.CatalogItems;

public class Create
{
    public class Command : IRequest<Guid>
    {

        public string Name { get; init; } = default!;

        public string Description { get; init; } = default!;

        public decimal Price { get; init; }

        public string PictureFileName { get; init; } = default!;

        public CatalogType? CatalogType { get; init; }

        public CatalogBrand? CatalogBrand { get; init; }

        public int AvailableStock { get; init; }

        public int RestockThreshold { get; init; }

        public int MaxStockThreshold { get; init; }

        public bool IsOnReorder { get; init; }
    }

    public class Handler : IRequestHandler<Command, Guid>
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly GuidService _guidService;

        public Handler(CatalogDbContext context, IMapper mapper, GuidService guidService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _guidService = guidService ?? throw new ArgumentNullException(nameof(guidService));
        }

        public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<CatalogItem>(request);
            item.SetId(_guidService.GetNewGuid());
            await _context.CatalogItems.InsertOneAsync(item, null, cancellationToken);

            return item.Id;
        }
    }
}
