namespace Catalog.API.Features.CatalogPictures;

public static class DownloadPicture
{
    public record Query(Guid id) : IRequest<PictureFile?>;

    public class Handler : IRequestHandler<Query, PictureFile?>
    {
        private const string DefaultContenType = "application/octet-stream";
        private readonly CatalogSettings _catalogSettings;
        private readonly ICatalogDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IContentTypeProvider _contentTypeProvider;
        private readonly IFileService _fileService;
        private readonly IValidator<Query> _validator;

        public Handler(
            ICatalogDbContext context,
            IContentTypeProvider contentTypeProvider,
            IWebHostEnvironment webHostEnvironment,
            IFileService fileService,
            IOptions<CatalogSettings> settings,
            IValidator<Query> validator)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _contentTypeProvider = contentTypeProvider ?? throw new ArgumentNullException(nameof(contentTypeProvider));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _catalogSettings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<PictureFile?> Handle(Query query, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(query);
            CatalogItem? item = await _db.FindAsync(query.id, cancellationToken);

            if (item is null)
            {
                return null;
            }

            string path = _fileService.PathCombine(_webHostEnvironment.WebRootPath, _catalogSettings.WebRootImagesPath, item.PictureFileName);

            if (!_fileService.FileExists(path))
            {
                return null;
            }

            byte[] buffer = await _fileService.FileReadAllBytesAsync(path, cancellationToken);

            return new PictureFile
            {
                Buffer = buffer,
                ContentType = GetImageMimeType(path)
            };
        }

        private string GetImageMimeType(string path)
        {
            if (!_contentTypeProvider.TryGetContentType(path, out string? contentType))
            {
                contentType = DefaultContenType;
            }

            return contentType;
        }
    }
}