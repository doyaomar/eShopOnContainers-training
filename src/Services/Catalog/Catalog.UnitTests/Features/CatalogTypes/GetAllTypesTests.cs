namespace Catalog.UnitTests.Features.CatalogTypes;

public class GetAllTypesTests
{
    readonly Mock<ICatalogDbContext> _dbStub;
    readonly Mock<IMapper> _mapperStub;
    readonly GetAllTypes.Handler _handler;

    public GetAllTypesTests()
    {
        _mapperStub = new();
        _dbStub = new();
        _handler = new(_dbStub.Object, _mapperStub.Object);
    }

    [Fact]
    public async Task Handle_WhenQueryIsValid_ThenReturnsCatalogTypeDto()
    {
        var idsStub = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var catalogTypeDtosMock = CatalogTypeFakes.GetCatalogTypeDtosFake(idsStub);
        var catalogTypesStub = CatalogTypeFakes.GetCatalogTypesFake(idsStub);
        _dbStub.Setup(db => db.FindAllCatalogTypesAsync(CancellationToken.None)).ReturnsAsync(catalogTypesStub);
        _mapperStub.Setup(mapper => mapper.Map<IReadOnlyCollection<CatalogTypeDto>>(catalogTypesStub)).Returns(catalogTypeDtosMock);

        var actual = await _handler.Handle(new GetAllTypes.Query(), CancellationToken.None);

        actual.Should().NotBeNullOrEmpty().And.Equal(catalogTypeDtosMock);
    }
}