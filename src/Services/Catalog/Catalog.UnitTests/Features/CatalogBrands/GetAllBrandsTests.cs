namespace Catalog.UnitTests.Features.CatalogBrands;

public class GetAllBrandsTests
{
    readonly Mock<ICatalogDbContext> _dbStub;
    readonly Mock<IMapper> _mapperStub;
    readonly GetAllBrands.Handler _handler;

    public GetAllBrandsTests()
    {
        _mapperStub = new();
        _dbStub = new();
        _handler = new(_dbStub.Object, _mapperStub.Object);
    }

    [Fact]
    public async Task Handle_WhenQueryIsValid_ThenReturnsCatalogBrandDtos()
    {
        var idsStub = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var catalogBrandDtosMock = CatalogBrandFakes.GetCatalogBrandDtosFake(idsStub);
        var catalogBrandsStub = CatalogBrandFakes.GetCatalogBrandsFake(idsStub);
        _dbStub.Setup(db => db.FindAllBrandsAsync(CancellationToken.None)).ReturnsAsync(catalogBrandsStub);
        _mapperStub.Setup(mapper => mapper.Map<IReadOnlyCollection<CatalogBrandDto>>(catalogBrandsStub)).Returns(catalogBrandDtosMock);

        var actual = await _handler.Handle(new GetAllBrands.Query(), CancellationToken.None);

        actual.Should().NotBeNullOrEmpty().And.Equal(catalogBrandDtosMock);
    }
}