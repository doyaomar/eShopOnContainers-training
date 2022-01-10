namespace Catalog.API.Features.CatalogPictures;

public class UploadPicture
{
    public class Command : IRequest<bool>
    {
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }

        [FromBody]
        public IFormFile PictureFile { get; set; } = default!;
    }

    public class Handler : IRequestHandler<Command, bool>
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

        public async Task<bool> Handle(Command command, CancellationToken cancellationToken)
        {
            _ = command ?? throw new ArgumentNullException(nameof(command));
            var item = await _db.FindAsync(command.Id, cancellationToken);

            if (item is null
            || !string.Equals(_fileService.PathGetExtension(item.PictureFileName), _fileService.PathGetExtension(command.PictureFile.FileName)))
            {
                return false;
            }

            var path = _fileService.PathCombine(_webHostEnvironment.WebRootPath, ImagesPath, item.PictureFileName);

            using (var stream = _fileService.FileCreate(path))
            {
                await command.PictureFile.CopyToAsync(stream);
            }

            return true;
        }
    }
}