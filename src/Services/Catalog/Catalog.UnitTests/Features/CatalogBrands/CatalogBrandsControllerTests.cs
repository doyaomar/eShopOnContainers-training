namespace Catalog.UnitTests.Features.CatalogBrands;

public class CatalogBrandsControllerTests
{
    readonly CatalogBrandsController _catalogBrandsController;
    readonly Mock<IMediator> _mediatorStub;

    public CatalogBrandsControllerTests()
    {
        _mediatorStub = new();
        Mock<ILogger<CatalogBrandsController>> logger = new();
        _catalogBrandsController = new(logger.Object, _mediatorStub.Object);
    }

    // GetCatalogBrandsAsync Tests

    [Fact]
    public async Task GetCatalogBrandsAsync_WhenCatalogBrandsExist_ThenReturnsOkObjectResult()
    {
        var catalogBrandDtosMock = CatalogBrandFakes.GetCatalogBrandDtosFake(new List<Guid> { Guid.NewGuid(), Guid.NewGuid() });
        _mediatorStub.Setup(mediator => mediator.Send(It.IsAny<GetAllBrands.Query>(), CancellationToken.None)).ReturnsAsync(catalogBrandDtosMock);

        var actual = await _catalogBrandsController.GetCatalogBrandsAsync(CancellationToken.None);

        actual.Result.Should().BeOfType<OkObjectResult>();
        ((actual.Result as OkObjectResult)!.Value as IReadOnlyCollection<CatalogBrandDto>)!.Should().Equal(catalogBrandDtosMock);
    }
}