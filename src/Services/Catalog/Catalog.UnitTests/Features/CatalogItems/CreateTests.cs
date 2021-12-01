namespace Catalog.UnitTests.Features.CatalogItems;

public class CreateTests
{
    readonly Mock<ICatalogDbContext> _dbStub;
    readonly Mock<IMapper> _mapperStub;
    readonly Mock<IGuidService> _guidServiceStub;
    readonly Create.Handler _handler;

    public CreateTests()
    {
        _dbStub = new();
        _mapperStub = new();
        _guidServiceStub = new();
        _handler = new(_dbStub.Object, _mapperStub.Object, _guidServiceStub.Object);
    }

    [Fact]
    public async Task Handle_When_Then()
    {
        var validRequestStub = CatalogItemFakes.GetCreateCommandFake();
        var validProductIdMock = Guid.NewGuid();
        var validCancellationTokenStub = CancellationToken.None;
        var catalogItemStub = CatalogItemFakes.GetCatalogItemFake();
        _mapperStub.Setup(mapper => mapper.Map<CatalogItem>(validRequestStub)).Returns(catalogItemStub);
        _guidServiceStub.Setup(svc => svc.GetNewGuid()).Returns(validProductIdMock);
        _dbStub.Setup(db => db.CatalogItems.InsertOneAsync(catalogItemStub, null, validCancellationTokenStub));

        var actual = await _handler.Handle(validRequestStub, validCancellationTokenStub);

        actual.Should().Be(validProductIdMock);
    }
}
