namespace Catalog.UnitTests.Features.CatalogItems;

public class GetByBrandTests
{
    readonly Mock<ICatalogDbContext> _dbStub;
    readonly Mock<IMapper> _mapperStub;
    readonly GetByBrand.Handler _handler;

    public GetByBrandTests()
    {
        _mapperStub = new();
        _dbStub = new();
        _handler = new(_dbStub.Object, _mapperStub.Object);
    }

    [Fact]
    public async Task Handle_WhenQueryIsValid_ThenReturnsPaginatedDto()
    {
        var idsStub = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var validQueryStub = CatalogItemFakes.GetByBrandQueryFake(Guid.NewGuid());
        var itemsStub = CatalogItemFakes.GetCatalogItemsFake(idsStub);
        var itemsDtoMock = CatalogItemFakes.GetCatalogItemDtosFake(idsStub);
        _dbStub.Setup(db => db.FindByBrandAsync(
            It.IsAny<Guid>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            CancellationToken.None)).ReturnsAsync((itemsStub, 2));
        _mapperStub.Setup(mapper => mapper.Map<IReadOnlyCollection<CatalogItemDto>>(itemsStub)).Returns(itemsDtoMock);

        var actual = await _handler.Handle(validQueryStub, CancellationToken.None);

        actual.Should().NotBeNull();
        actual.Count.Should().Be(2);
        actual.Items.Should().NotBeNullOrEmpty();
        actual.Items.Should().Equal(itemsDtoMock);
    }
}