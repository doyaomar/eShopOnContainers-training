namespace Catalog.UnitTests.Features.CatalogPictures;

public class DownloadPictureTests
{
    readonly Mock<ICatalogDbContext> _dbStub;
    readonly Mock<IMapper> _mapperStub;
    private readonly Mock<IOptions<CatalogSettings>> _catalogSettingsStub;
    private readonly Mock<IWebHostEnvironment> _webHostEnvironmentStub;
    private readonly Mock<IContentTypeProvider> _contentTypeProviderStub;
    private readonly Mock<IFileService> _fileServiceStub;
    readonly DownloadPicture.Handler _handler;

    public DownloadPictureTests()
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
    public async Task Handle_WhenQueryIsValidAndPictureExists_ThenReturnsPictureFile()
    {
        var validPictureFileMock = CatalogPictureFakes.GetPictureFileFake();
        var validProductIdStub = Guid.NewGuid();
        var contentTypeMock = "image/png";
        var bufferMock = new byte[] { 0x00, 0x01, 0x02, 0x03 };
        var validQueryStub = new DownloadPicture.Query(validProductIdStub);
        var catalogItemStub = CatalogItemFakes.GetCatalogItemFake();
        _contentTypeProviderStub.Setup(svc => svc.TryGetContentType(It.IsAny<string>(), out contentTypeMock)).Returns(true);
        _dbStub.Setup(db => db.FindAsync(validProductIdStub, CancellationToken.None)).ReturnsAsync(catalogItemStub);
        _fileServiceStub.Setup(svc => svc.PathCombine(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns("path.png");
        _fileServiceStub.Setup(svc => svc.FileExists(It.IsAny<string>())).Returns(true);
        _fileServiceStub.Setup(svc => svc.FileReadAllBytesAsync(It.IsAny<string>(), CancellationToken.None)).ReturnsAsync(bufferMock);

        var actual = await _handler.Handle(validQueryStub, CancellationToken.None);

        actual!.Buffer.Should().Equal(bufferMock);
        actual.ContentType.Should().Be(contentTypeMock);
    }

    [Fact]
    public async Task Handle_WhenQueryIsValidAndProductDoesntExist_ThenReturnsNull()
    {
        var validProductIdStub = Guid.NewGuid();
        var validQueryStub = new DownloadPicture.Query(validProductIdStub);
        var catalogItemStub = CatalogItemFakes.GetCatalogItemFake();
        _dbStub.Setup(db => db.FindAsync(validProductIdStub, CancellationToken.None)).ReturnsAsync(catalogItemStub);
        _fileServiceStub.Setup(svc => svc.PathCombine(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns("path.png");
        _fileServiceStub.Setup(svc => svc.FileExists(It.IsAny<string>())).Returns(false);

        var actual = await _handler.Handle(validQueryStub, CancellationToken.None);

        actual.Should().BeNull();
    }

    [Fact]
    public async Task Handle_WhenQueryIsValidAndFileDoesntExist_ThenReturnsNull()
    {
        var validProductIdStub = Guid.NewGuid();
        var validQueryStub = new DownloadPicture.Query(validProductIdStub);
        CatalogItem catalogItemStub = null!;
        _dbStub.Setup(db => db.FindAsync(validProductIdStub, CancellationToken.None)).ReturnsAsync(catalogItemStub);

        var actual = await _handler.Handle(validQueryStub, CancellationToken.None);

        actual.Should().BeNull();
    }
}