namespace Catalog.API.Features.CatalogItems;

public class GetPicture
{
    public record Query(Guid id) : IRequest<PictureFile?>;

    public class Handler : IRequestHandler<Query, PictureFile?>
    {
        private const string DefaultContenType = "application/octet-stream";
        private const string ImagesPath = "images";
        private readonly ICatalogDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IContentTypeProvider _contentTypeProvider;

        public Handler(ICatalogDbContext context, IContentTypeProvider contentTypeProvider, IWebHostEnvironment webHostEnvironment)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _contentTypeProvider = contentTypeProvider ?? throw new ArgumentNullException(nameof(contentTypeProvider));
        }

        public async Task<PictureFile?> Handle(Query query, CancellationToken cancellationToken)
        {
            _ = query ?? throw new ArgumentNullException(nameof(query));
            var item = await _db.FindAsync(query.id, cancellationToken);

            if (item is null)
            {
                return null;
            }

            var path = Path.Combine(_webHostEnvironment.WebRootPath, ImagesPath, item.PictureFileName);

            if (!_contentTypeProvider.TryGetContentType(path, out string? contentType))
            {
                contentType = DefaultContenType;
            }

            var buffer = await File.ReadAllBytesAsync(path, cancellationToken);

            return new PictureFile
            {
                Buffer = buffer,
                ContentType = contentType
            };
        }
    }
}