namespace Catalog.UnitTests.Features.CatalogItems;

public class UpdateTests
{
    readonly Mock<ICatalogDbContext> _dbStub;
    readonly Update.Handler _handler;
    readonly Mock<IMapper> _mapperStub;
    readonly Mock<IFileService> _fileServiceStub;


    public UpdateTests()
    {
        _mapperStub = new();
        _dbStub = new();
        _fileServiceStub = new();
        _handler = new(_dbStub.Object, _mapperStub.Object, _fileServiceStub.Object);
    }

    public async Task Handle_WhenRequestIsValidAndProductExists_ThenReturnsTrue()
    {
        Update.Command validRequestStub = new();
        var catalogItemStub = CatalogItemFakes.GetCatalogItemFake();
        _mapperStub.Setup(mapper => mapper.Map<CatalogItem>(validRequestStub)).Returns(catalogItemStub);
        _fileServiceStub.Setup(svc => svc.PathGetExtension(It.IsAny<string>())).Returns(".png");
        _dbStub.Setup(db => db.FindOneAndReplaceAsync(catalogItemStub, CancellationToken.None)).ReturnsAsync(catalogItemStub);

        var actual = await _handler.Handle(validRequestStub, CancellationToken.None);

        actual.Should().BeTrue();
    }

    public async Task Handle_WhenRequestIsValidAndProductDoesntExist_ThenReturnsFalse()
    {
        Update.Command validRequestStub = new();
        var catalogItemStub = CatalogItemFakes.GetCatalogItemFake();
        _mapperStub.Setup(mapper => mapper.Map<CatalogItem>(validRequestStub)).Returns(catalogItemStub);
        _fileServiceStub.Setup(svc => svc.PathGetExtension(It.IsAny<string>())).Returns(".png");
        _dbStub.Setup(db => db.FindOneAndReplaceAsync(catalogItemStub, CancellationToken.None)).ReturnsAsync((CatalogItem)null!);

        var actual = await _handler.Handle(validRequestStub, CancellationToken.None);

        actual.Should().BeFalse();
    }
}