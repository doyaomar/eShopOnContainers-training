namespace Catalog.API.Features.CatalogPictures;

public class DownloadPicture
{
    public record Query(Guid id) : IRequest<PictureFile?>;

    public class Handler : IRequestHandler<Query, PictureFile?>
    {
        private const string DefaultContenType = "application/octet-stream";
        private const string ImagesPath = "images";
        private readonly ICatalogDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IContentTypeProvider _contentTypeProvider;
        private readonly IFileService _fileService;

        public Handler(ICatalogDbContext context, IContentTypeProvider contentTypeProvider, IWebHostEnvironment webHostEnvironment, IFileService fileService)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _contentTypeProvider = contentTypeProvider ?? throw new ArgumentNullException(nameof(contentTypeProvider));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        public async Task<PictureFile?> Handle(Query query, CancellationToken cancellationToken)
        {
            _ = query ?? throw new ArgumentNullException(nameof(query));
            var item = await _db.FindAsync(query.id, cancellationToken);

            if (item is null)
            {
                return null;
            }

            string path = _fileService.PathCombine(_webHostEnvironment.WebRootPath, ImagesPath, item.PictureFileName);

            if (!_fileService.FileExists(path))
            {
                return null;
            }

            if (!_contentTypeProvider.TryGetContentType(path, out string? contentType))
            {
                contentType = DefaultContenType;
            }

            var buffer = await _fileService.FileReadAllBytesAsync(path, cancellationToken);

            return new PictureFile
            {
                Buffer = buffer,
                ContentType = contentType
            };
        }
    }
}