namespace Catalog.UnitTests.Features.CatalogPictures;

public class UploadPictureTests
{
    private readonly Mock<ICatalogDbContext> _dbStub;
    private readonly Mock<IOptions<CatalogSettings>> _catalogSettingsStub;
    private readonly Mock<IWebHostEnvironment> _webHostEnvironmentStub;
    private readonly Mock<IFileService> _fileServiceStub;
    private readonly UploadPicture.Handler _handler;
    private readonly IValidator<UploadPicture.Command> _validator;

    public UploadPictureTests()
    {
        _dbStub = new();
        _catalogSettingsStub = new();
        _webHostEnvironmentStub = new();
        _fileServiceStub = new();
        _catalogSettingsStub.SetReturnsDefault(new CatalogSettings
        {
            WebRootImagesPath = "images",
            CatalogItemPictureSizeLimit = 1000
        });
        _validator = new UploadPictureValidator(_catalogSettingsStub.Object);

        _handler = new(
            _dbStub.Object,
            _webHostEnvironmentStub.Object,
            _fileServiceStub.Object,
            _catalogSettingsStub.Object,
            _validator);
    }

    //[Fact]
    //public async Task Hanlde_WhenCommandIsValidAndProductExists_ThenReturnsTrue()
    //{
    //    var validProductIdStub = Guid.NewGuid();
    //    var validCommandStub = CatalogPictureFakes.GetUploadPictureCommandFake(validProductIdStub);
    //    var pathStub = "path.png";
    //    var fileStreamStub = new Mock<FileStream>(pathStub, FileMode.OpenOrCreate);
    //    var catalogItemStub = CatalogItemFakes.GetCatalogItemFake();
    //    _dbStub.Setup(db => db.FindAsync(validProductIdStub, CancellationToken.None)).ReturnsAsync(catalogItemStub);
    //    _fileServiceStub.Setup(svc => svc.PathGetExtension(It.IsAny<string>())).Returns(".png");
    //    _fileServiceStub.Setup(svc => svc.PathCombine(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(pathStub);
    //    _fileServiceStub.Setup(svc => svc.FileCreate(It.IsAny<string>())).Returns(fileStreamStub.Object);

    //    var actual = await _handler.Handle(validCommandStub, CancellationToken.None);

    //    actual.Should().BeTrue();
    //}

    //[Fact]
    //public async Task Hanlde_WhenCommandIsValidAndProductDoesntExist_ThenReturnsFalse()
    //{
    //    var validProductIdStub = Guid.NewGuid();
    //    var validCommandStub = CatalogPictureFakes.GetUploadPictureCommandFake(validProductIdStub);
    //    CatalogItem catalogItemStub = null!;
    //    _dbStub.Setup(db => db.FindAsync(validProductIdStub, CancellationToken.None)).ReturnsAsync(catalogItemStub);

    //    var actual = await _handler.Handle(validCommandStub, CancellationToken.None);

    //    actual.Should().BeFalse();
    //}

    [Fact]
    public async Task Hanlde_WhenCommandIsNull_ThenThrowsArgumentNullException()
    {
        UploadPicture.Command invalidCommandStub = null!;

        Func<Task> actual = async () => await _handler.Handle(invalidCommandStub, CancellationToken.None);

        await actual.Should().ThrowAsync<ArgumentNullException>();
    }
}
