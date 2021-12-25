namespace Catalog.UnitTests.Features.CatalogTypes;

public class CatalogTypesControllerTests
{
    readonly CatalogTypesController _catalogTypesController;
    readonly Mock<IMediator> _mediatorStub;

    public CatalogTypesControllerTests()
    {
        _mediatorStub = new();
        Mock<ILogger<CatalogTypesController>> logger = new();
        _catalogTypesController = new(logger.Object, _mediatorStub.Object);
    }

    // GetCatalogTypesAsync Tests

    [Fact]
    public async Task GetCatalogTypesAsync_WhenCatalogTypesExist_ThenReturnsOkObjectResult()
    {
        var catalogTypeDtosMock = CatalogTypeFakes.GetCatalogTypeDtosFake(new List<Guid> { Guid.NewGuid(), Guid.NewGuid() });
        _mediatorStub.Setup(mediator => mediator.Send(It.IsAny<GetAllTypes.Query>(), CancellationToken.None)).ReturnsAsync(catalogTypeDtosMock);

        var actual = await _catalogTypesController.GetCatalogTypesAsync(CancellationToken.None);

        actual.Result.Should().BeOfType<OkObjectResult>();
        ((actual.Result as OkObjectResult)!.Value as IReadOnlyCollection<CatalogTypeDto>)!.Should().Equal(catalogTypeDtosMock);
    }
}