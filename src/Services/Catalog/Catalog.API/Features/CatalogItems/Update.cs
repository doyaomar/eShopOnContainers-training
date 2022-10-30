namespace Catalog.API.Features.CatalogItems;

public static class Update
{
    public class Command : IRequest<bool>
    {
        public Guid Id { get; init; }

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

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly ICatalogDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IValidator<Command> _validator;

        public Handler(ICatalogDbContext context, IMapper mapper, IFileService fileService, IValidator<Command> validator)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<bool> Handle(Command command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);
            var item = _mapper.Map<CatalogItem>(command);
            item.GeneratePictureFileName(_fileService.PathGetExtension(item.PictureFileName));
            CatalogItem? updatedItem = await _db.FindOneAndReplaceAsync(item, cancellationToken);

            return updatedItem is not null;
        }
    }
}