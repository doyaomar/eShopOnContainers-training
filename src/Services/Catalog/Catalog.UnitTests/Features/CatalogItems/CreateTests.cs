namespace Catalog.UnitTests.Features.CatalogItems;

public class CreateTests
{
    readonly Mock<ICatalogDbContext> _dbStub;
    readonly Mock<IMapper> _mapperStub;
    readonly Mock<IGuidService> _guidServiceStub;
    readonly Mock<IFileService> _fileServiceStub;
    readonly Create.Handler _handler;

    public CreateTests()
    {
        _dbStub = new();
        _mapperStub = new();
        _guidServiceStub = new();
        _fileServiceStub = new();
        _handler = new(_dbStub.Object, _mapperStub.Object, _guidServiceStub.Object, _fileServiceStub.Object);
    }

    [Fact]
    public async Task Handle_WhenRequestIsValid_ThenReturnsNewId()
    {
        var validRequestStub = CatalogItemFakes.GetCreateCommandFake();
        var validProductIdMock = Guid.NewGuid();
        var catalogItemStub = CatalogItemFakes.GetCatalogItemFake();
        _mapperStub.Setup(mapper => mapper.Map<CatalogItem>(validRequestStub)).Returns(catalogItemStub);
        _guidServiceStub.Setup(svc => svc.GetNewGuid()).Returns(validProductIdMock);
        _fileServiceStub.Setup(svc => svc.PathGetExtension(It.IsAny<string>())).Returns(".png");
        _dbStub.Setup(db => db.InsertOneAsync(catalogItemStub, CancellationToken.None)).ReturnsAsync(validProductIdMock);

        var actual = await _handler.Handle(validRequestStub, CancellationToken.None);

        actual.Should().Be(validProductIdMock);
    }
}
