namespace Catalog.UnitTests.Features.CatalogItems;

public class GetByIdTests
{
    readonly Mock<ICatalogDbContext> _dbStub;
    readonly Mock<IMapper> _mapperStub;
    readonly GetById.Handler _handler;

    public GetByIdTests()
    {
        _mapperStub = new();
        _dbStub = new();
        _handler = new(_dbStub.Object, _mapperStub.Object);
    }

    [Fact]
    public async Task Handle_WhenQueryIsValid_ThenReturnsCalaogItemDto()
    {
        var validProductIdStub = Guid.NewGuid();
        GetById.Query validRequestStub = new(validProductIdStub);
        var catalogItemStub = CatalogItemFakes.GetCatalogItemFake();
        var catalogItemDtoMock = CatalogItemFakes.GetCatalogItemDtoFake(validProductIdStub);
        _dbStub.Setup(db => db.FindAsync(validProductIdStub, CancellationToken.None)).ReturnsAsync(catalogItemStub);
        _mapperStub.Setup(mapper => mapper.Map<CatalogItemDto>(catalogItemStub)).Returns(catalogItemDtoMock);

        var actual = await _handler.Handle(validRequestStub, CancellationToken.None);

        actual.Should().NotBeNull();
        actual!.Id.Should().Be(catalogItemDtoMock.Id);
        actual!.Name.Should().Be(catalogItemDtoMock.Name);
    }

    [Fact]
    public async Task Handle_WhenQueryIsValidAndProductDoesntExist_ThenReturnsNull()
    {
        var validProductIdStub = Guid.NewGuid();
        GetById.Query validRequestStub = new(validProductIdStub);
        var catalogItemStub = CatalogItemFakes.GetCatalogItemFake();
        var catalogItemDtoMock = CatalogItemFakes.GetCatalogItemDtoFake(validProductIdStub);
        _dbStub.Setup(db => db.FindAsync(validProductIdStub, CancellationToken.None)).ReturnsAsync((CatalogItem)null!);
        _mapperStub.Setup(mapper => mapper.Map<CatalogItemDto>(catalogItemStub)).Returns((CatalogItemDto)null!);

        var actual = await _handler.Handle(validRequestStub, CancellationToken.None);

        actual.Should().BeNull();
    }

    [Fact]
    public async Task Handle_WhenQueryIsNull_ThenThrowsException()
    {
        GetById.Query invalidRequestStub = null!;

        Func<Task> actual = async () => await _handler.Handle(invalidRequestStub, CancellationToken.None);

        await actual.Should().ThrowAsync<ArgumentNullException>();
    }
}