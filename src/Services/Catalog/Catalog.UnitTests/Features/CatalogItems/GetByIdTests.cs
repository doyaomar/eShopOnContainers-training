namespace Catalog.UnitTests.Features.CatalogItems;

public class GetByIdTests
{
    private readonly Mock<ICatalogDbContext> _dbStub;
    private readonly Mock<IMapper> _mapperStub;
    private readonly GetById.Handler _handler;
    private readonly IValidator<GetById.Query> _validator;

    public GetByIdTests()
    {
        _mapperStub = new();
        _dbStub = new();
        _validator = new GetByIdValidator();
        _handler = new(_dbStub.Object, _mapperStub.Object, _validator);
    }

    [Fact]
    public async Task Handle_WhenQueryIsValid_ThenReturnsCalaogItemDto()
    {
        var validProductIdStub = Guid.NewGuid();
        GetById.Query validQueryStub = new(validProductIdStub);
        var catalogItemStub = CatalogItemFakes.GetCatalogItemFake();
        var catalogItemDtoMock = CatalogItemFakes.GetCatalogItemDtoFake(validProductIdStub);
        _dbStub.Setup(db => db.FindAsync(validProductIdStub, CancellationToken.None)).ReturnsAsync(catalogItemStub);
        _mapperStub.Setup(mapper => mapper.Map<CatalogItemDto>(catalogItemStub)).Returns(catalogItemDtoMock);

        var actual = await _handler.Handle(validQueryStub, CancellationToken.None);

        actual.Should().NotBeNull();
        actual!.Id.Should().Be(catalogItemDtoMock.Id);
        actual!.Name.Should().Be(catalogItemDtoMock.Name);
    }

    [Fact]
    public async Task Handle_WhenQueryIsValidAndProductDoesntExist_ThenReturnsNull()
    {
        var validProductIdStub = Guid.NewGuid();
        GetById.Query validQueryStub = new(validProductIdStub);
        var catalogItemStub = CatalogItemFakes.GetCatalogItemFake();
        _dbStub.Setup(db => db.FindAsync(validProductIdStub, CancellationToken.None)).ReturnsAsync((CatalogItem)null!);
        _mapperStub.Setup(mapper => mapper.Map<CatalogItemDto>(catalogItemStub)).Returns((CatalogItemDto)null!);

        var actual = await _handler.Handle(validQueryStub, CancellationToken.None);

        actual.Should().BeNull();
    }

    [Fact]
    public async Task Handle_WhenQueryIsNull_ThenThrowsArgumentNullException()
    {
        GetById.Query invalidQueryStub = null!;

        Func<Task> actual = async () => await _handler.Handle(invalidQueryStub, CancellationToken.None);

        await actual.Should().ThrowAsync<ArgumentNullException>();
    }
}