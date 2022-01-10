namespace Catalog.API.Features.CatalogItems;

public class Create
{
    public class Command : IRequest<Guid>
    {
        public string Name { get; init; } = default!;

        public string Description { get; init; } = default!;

        public decimal Price { get; init; }

        public string PictureFileName { get; init; } = default!;

        public CatalogTypeDto CatalogType { get; init; } = default!;

        public CatalogBrandDto CatalogBrand { get; init; } = default!;

        public int AvailableStock { get; init; }

        public int RestockThreshold { get; init; }

        public int MaxStockThreshold { get; init; }

        public bool IsOnReorder { get; init; }
    }

    public class Handler : IRequestHandler<Command, Guid>
    {
        private readonly ICatalogDbContext _db;
        private readonly IMapper _mapper;
        private readonly IGuidService _guidService;
        private readonly IFileService _fileService;

        public Handler(ICatalogDbContext context, IMapper mapper, IGuidService guidService, IFileService fileService)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _guidService = guidService ?? throw new ArgumentNullException(nameof(guidService));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        public async Task<Guid> Handle(Command command, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<CatalogItem>(command);
            item.SetId(_guidService.GetNewGuid());
            item.GeneratePictureFileName(_fileService.PathGetExtension(item.PictureFileName));

            return await _db.InsertOneAsync(item, cancellationToken);
        }
    }
}
