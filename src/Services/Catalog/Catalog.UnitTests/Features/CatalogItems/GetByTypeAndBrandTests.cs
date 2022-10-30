namespace Catalog.UnitTests.Features.CatalogItems;

public class GetByTypeAndBrandTests
{
    private readonly Mock<ICatalogDbContext> _dbStub;
    private readonly Mock<IMapper> _mapperStub;
    private readonly GetByTypeAndBrand.Handler _handler;
    private readonly IValidator<GetByTypeAndBrand.Query> _validator;

    public GetByTypeAndBrandTests()
    {
        _mapperStub = new();
        _dbStub = new();
        _validator = new GetByTypeAndBrandValidator();
        _handler = new(_dbStub.Object, _mapperStub.Object, _validator);
    }

    [Fact]
    public async Task Handle_WhenQueryIsValid_ThenReturnsPaginatedDto()
    {
        var idsStub = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var validQueryStub = CatalogItemFakes.GetByTypeAndBrandQueryFake(Guid.NewGuid(), Guid.NewGuid());
        var itemsStub = CatalogItemFakes.GetCatalogItemsFake(idsStub);
        var itemsDtoMock = CatalogItemFakes.GetCatalogItemDtosFake(idsStub);
        _dbStub.Setup(db => db.FindByTypeAndBrandAsync(
            It.IsAny<Guid>(),
            It.IsAny<Guid?>(),
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

    [Fact]
    public async Task Handle_WhenQueryIsNull_ThenThrowsArgumentNullException()
    {
        GetByTypeAndBrand.Query invalidQueryStub = null!;

        Func<Task> actual = async () => await _handler.Handle(invalidQueryStub, CancellationToken.None);

        await actual.Should().ThrowAsync<ArgumentNullException>();
    }
}