namespace Catalog.UnitTests.Features.CatalogPictures;

public class CatalogPicturesControllerTests
{
    readonly CatalogPicturesController _catalogPicturesController;
    readonly Mock<IMediator> _mediatorStub;

    public CatalogPicturesControllerTests()
    {
        _mediatorStub = new();
        Mock<ILogger<CatalogPicturesController>> logger = new();
        _catalogPicturesController = new(logger.Object, _mediatorStub.Object);
    }

    // DownloadCatalogItemPictureAsync Tests

    [Fact]
    public async Task DownloadCatalogItemPictureAsync_WhenQueryIsValidAndPictureExists_ThenReturnsFileContentResult()
    {
        var validQueryStub = new DownloadPicture.Query(Guid.NewGuid());
        var validPictureFileMock = CatalogPictureFakes.GetPictureFileFake();
        _mediatorStub.Setup(m => m.Send(validQueryStub, CancellationToken.None)).ReturnsAsync(validPictureFileMock);

        var actual = await _catalogPicturesController.DownloadCatalogItemPictureAsync(validQueryStub, CancellationToken.None);

        actual.Should().BeOfType<FileContentResult>();
    }

    [Fact]
    public async Task DownloadCatalogItemPictureAsync_WhenQueryIsValidAndPictureDoesntExist_ThenReturnsNotFoundResult()
    {
        var validQueryStub = new DownloadPicture.Query(Guid.NewGuid());

        var actual = await _catalogPicturesController.DownloadCatalogItemPictureAsync(validQueryStub, CancellationToken.None);

        actual.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task DownloadCatalogItemPictureAsync_WhenQueryIsNotValid_ThenReturnsBadRequestObjectResult()
    {
        var invalidQueryStub = new DownloadPicture.Query(Guid.Empty);
        _mediatorStub.Setup(m => m.Send(invalidQueryStub, CancellationToken.None)).ThrowsAsync(new ValidationException("invalid error"));

        var actual = await _catalogPicturesController.DownloadCatalogItemPictureAsync(invalidQueryStub, CancellationToken.None);

        actual.Should().BeOfType<BadRequestObjectResult>();
    }

    // UploadCatalogItemPictureAsync Tests

    [Fact]
    public async Task UploadCatalogItemPictureAsync_WhenQueryIsValid_ThenReturnsCreatedAtActionResult()
    {
        var validCommandStub = CatalogPictureFakes.GetUploadPictureCommandFake(Guid.NewGuid());
        _mediatorStub.Setup(m => m.Send(validCommandStub, CancellationToken.None)).ReturnsAsync(true);

        var actual = await _catalogPicturesController.UploadCatalogItemPictureAsync(validCommandStub);

        actual.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public async Task UploadCatalogItemPictureAsync_WhenQueryIsValidAndPictureDoesntExist_ThenReturnsNotFoundResult()
    {
        var validCommandStub = CatalogPictureFakes.GetUploadPictureCommandFake(Guid.NewGuid());
        _mediatorStub.Setup(m => m.Send(validCommandStub, CancellationToken.None)).ReturnsAsync(false);

        var actual = await _catalogPicturesController.UploadCatalogItemPictureAsync(validCommandStub);

        actual.Should().BeOfType<NotFoundResult>();
    }
}