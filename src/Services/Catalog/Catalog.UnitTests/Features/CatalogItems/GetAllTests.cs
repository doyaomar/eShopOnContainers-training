namespace Catalog.UnitTests.Features.CatalogItems;

public class GetAllTests
{
    readonly Mock<ICatalogDbContext> _dbStub;
    readonly Mock<IMapper> _mapperStub;
    readonly GetAll.Handler _handler;
    private readonly IValidator<GetAll.Query> _validator;

    public GetAllTests()
    {
        _mapperStub = new();
        _dbStub = new();
        _validator = new GetAllValidator();
        _handler = new(_dbStub.Object, _mapperStub.Object, _validator);
    }

    [Fact]
    public async Task Handle_WhenQueryIsValid_ThenReturnsPaginatedDto()
    {
        var idsStub = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var validQueryStub = CatalogItemFakes.GetGetAllQueryFake(string.Join(';', idsStub));
        var itemsStub = CatalogItemFakes.GetCatalogItemsFake(idsStub);
        var itemsDtoMock = CatalogItemFakes.GetCatalogItemDtosFake(idsStub);
        _dbStub.Setup(db => db.FindAllAsync(
            It.IsAny<IEnumerable<Guid>>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            CancellationToken.None)).ReturnsAsync((itemsStub, 2));
        _mapperStub.Setup(mapper => mapper.Map<IReadOnlyCollection<CatalogItemDto>>(itemsStub)).Returns(itemsDtoMock);

        var actual = await _handler.Handle(validQueryStub, CancellationToken.None);

        actual.Should().NotBeNull();
        actual.Count.Should().Be(2);
        actual.Items.Should().NotBeNullOrEmpty().And.Equal(itemsDtoMock);
    }
}