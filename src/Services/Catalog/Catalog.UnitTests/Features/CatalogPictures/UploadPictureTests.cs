namespace Catalog.UnitTests.Features.CatalogPictures;

public class UploadPictureTests
{
    readonly Mock<ICatalogDbContext> _dbStub;
    readonly Mock<IMapper> _mapperStub;
    private readonly Mock<IOptions<CatalogSettings>> _catalogSettingsStub;
    private readonly Mock<IWebHostEnvironment> _webHostEnvironmentStub;
    private readonly Mock<IContentTypeProvider> _contentTypeProviderStub;
    private readonly Mock<IFileService> _fileServiceStub;
    readonly UploadPicture.Handler _handler;

    public UploadPictureTests()
    {
        _dbStub = new();
        _mapperStub = new();
        _catalogSettingsStub = new();
        _webHostEnvironmentStub = new();
        _contentTypeProviderStub = new();
        _fileServiceStub = new();

        _catalogSettingsStub.SetReturnsDefault(new CatalogSettings
        {
            WebRootImagesPath = "images"
        });

        _handler = new(_dbStub.Object, _contentTypeProviderStub.Object, _webHostEnvironmentStub.Object, _fileServiceStub.Object, _catalogSettingsStub.Object);
    }

    [Fact]
    public async Task Hanlde_WhenCommandIsValidAndProductExists_ThenReturnsTrue()
    {
        var validProductIdStub = Guid.NewGuid();
        var validCommandStub = CatalogPictureFakes.GetUploadPictureCommandFake(validProductIdStub);
        var pathStub = "path.png";
        var fileStreamStub = new FileStream(pathStub, FileMode.Open);
        var catalogItemStub = CatalogItemFakes.GetCatalogItemFake();
        _dbStub.Setup(db => db.FindAsync(validProductIdStub, CancellationToken.None)).ReturnsAsync(catalogItemStub);
        _fileServiceStub.Setup(svc => svc.PathCombine(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(pathStub);
        _fileServiceStub.Setup(svc => svc.PathGetExtension(It.IsAny<string>())).Returns(".png");
        _fileServiceStub.Setup(svc => svc.FileCreate(It.IsAny<string>())).Returns(fileStreamStub);

        var actual = await _handler.Handle(validCommandStub, CancellationToken.None);

        actual.Should().BeTrue();
    }

    [Fact]
    public async Task Hanlde_WhenCommandIsValidAndProductDoesntExist_ThenReturnsFalse()
    {
        var validProductIdStub = Guid.NewGuid();
        var validCommandStub = CatalogPictureFakes.GetUploadPictureCommandFake(validProductIdStub);
        CatalogItem catalogItemStub = null!;
        _dbStub.Setup(db => db.FindAsync(validProductIdStub, CancellationToken.None)).ReturnsAsync(catalogItemStub);

        var actual = await _handler.Handle(validCommandStub, CancellationToken.None);

        actual.Should().BeFalse();
    }
}