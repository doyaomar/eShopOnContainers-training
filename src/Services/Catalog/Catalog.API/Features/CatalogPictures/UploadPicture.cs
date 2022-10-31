namespace Catalog.API.Features.CatalogPictures;

public static class UploadPicture
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
        private readonly CatalogSettings _catalogSettings;
        private readonly ICatalogDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        private readonly IValidator<Command> _validator;

        public Handler(
            ICatalogDbContext context,
            IWebHostEnvironment webHostEnvironment,
            IFileService fileService,
            IOptions<CatalogSettings> settings,
            IValidator<Command> validator)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _catalogSettings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<bool> Handle(Command command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);
            CatalogItem? item = await _db.FindAsync(command.Id, cancellationToken);

            if (item is null
            || !string.Equals(_fileService.PathGetExtension(item.PictureFileName), _fileService.PathGetExtension(command.PictureFile.FileName)))
            {
                return false;
            }

            string path = _fileService.PathCombine(_webHostEnvironment.WebRootPath, _catalogSettings.WebRootImagesPath, item.PictureFileName);
            using FileStream stream = _fileService.FileCreate(path);
            await command.PictureFile.CopyToAsync(stream, cancellationToken);

            return true;
        }
    }
}