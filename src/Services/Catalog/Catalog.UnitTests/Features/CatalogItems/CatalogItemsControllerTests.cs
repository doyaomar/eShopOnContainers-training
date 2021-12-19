namespace Catalog.UnitTests.Features.CatalogItems;

public class CatalogItemsControllerTests
{
    readonly CatalogItemsController _catalogItemsController;
    readonly Mock<IMediator> _mediatorStub;

    public CatalogItemsControllerTests()
    {
        _mediatorStub = new();
        Mock<ILogger<CatalogItemsController>> logger = new();
        _catalogItemsController = new(logger.Object, _mediatorStub.Object);
    }

    // CreateProductAsync Tests

    [Fact]
    public async Task CreateProductAsync_WhenCreateRequestIsValidAndProductExists_ThenReturnsCreatedAtActionResult()
    {
        var validRequestStub = CatalogItemFakes.GetCreateCommandFake();
        var validProductIdMock = Guid.NewGuid();
        _mediatorStub.Setup(mediator => mediator.Send(validRequestStub, CancellationToken.None)).ReturnsAsync(validProductIdMock);

        var actual = await _catalogItemsController.CreateCatalogItemAsync(validRequestStub);

        actual.Should().BeOfType<CreatedAtActionResult>();
        (actual as CreatedAtActionResult)!.ActionName.Should().Be("GetCatalogItemAsync");
        (actual as CreatedAtActionResult)!.RouteValues!.Values.Should().Contain(validProductIdMock);
    }

    // UpdateCatalogItemAsync Tests

    [Fact]
    public async Task UpdateCatalogItemAsync_WhenUpdateRequestIsValidAndProductExists_ThenReturnsNoContentResult()
    {
        var validProductIdStub = Guid.NewGuid();
        var validRequestStub = CatalogItemFakes.GetUpdateCommandFake(validProductIdStub);
        var updatedStub = true;
        _mediatorStub.Setup(mediator => mediator.Send(validRequestStub, CancellationToken.None)).ReturnsAsync(updatedStub);

        var actual = await _catalogItemsController.UpdateCatalogItemAsync(validProductIdStub, validRequestStub);

        actual.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task UpdateCatalogItemAsync_WhenUpdateRequestIsValidAndProductDoesntExist_ThenReturnsNotFoundResult()
    {
        var validProductIdStub = Guid.NewGuid();
        var validRequestStub = CatalogItemFakes.GetUpdateCommandFake(validProductIdStub);
        var updatedStub = false;
        _mediatorStub.Setup(mediator => mediator.Send(validRequestStub, CancellationToken.None)).ReturnsAsync(updatedStub);

        var actual = await _catalogItemsController.UpdateCatalogItemAsync(validProductIdStub, validRequestStub);

        actual.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task UpdateCatalogItemAsync_WhenUpdateRequestIsNotValid_ThenReturnsBadRequestResult()
    {
        var validProductIdStub = Guid.NewGuid();
        var invalidRequestStub = CatalogItemFakes.GetUpdateCommandFake(Guid.NewGuid());

        var actual = await _catalogItemsController.UpdateCatalogItemAsync(validProductIdStub, invalidRequestStub);

        actual.Should().BeOfType<BadRequestResult>();
    }

    // DeleteCatalogItemAsync Tests

    [Fact]
    public async Task DeleteCatalogItemAsync_WhenDeleteRequestIsValidAndProductExists_ThenReturnsNoContentResult()
    {
        var validProductIdStub = Guid.NewGuid();
        var validRequestStub = new Delete.Command(validProductIdStub);
        var deletedStub = true;
        _mediatorStub.Setup(mediator => mediator.Send(validRequestStub, CancellationToken.None)).ReturnsAsync(deletedStub);

        var actual = await _catalogItemsController.DeleteCatalogItemAsync(validRequestStub);

        actual.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task DeleteCatalogItemAsync_WhenDeleteRequestIsValidAndProductDoesntExist_ThenReturnsNotFoundResult()
    {
        var validProductIdStub = Guid.NewGuid();
        var validRequestStub = new Delete.Command(validProductIdStub);
        var deletedStub = false;
        _mediatorStub.Setup(mediator => mediator.Send(validRequestStub, CancellationToken.None)).ReturnsAsync(deletedStub);

        var actual = await _catalogItemsController.DeleteCatalogItemAsync(validRequestStub);

        actual.Should().BeOfType<NotFoundResult>();
    }

    // GetCatalogItemAsync Tests

    [Fact]
    public async Task GetCatalogItemAsync_WhenQueryIsValidAndProductExists_ThenReturnsOkObjectResult()
    {
        var validProductIdStub = Guid.NewGuid();
        var validRequestStub = new GetById.Query(validProductIdStub);
        var catalogItemDtoMock = CatalogItemFakes.GetCatalogItemDtoFake(validProductIdStub);
        _mediatorStub.Setup(mediator => mediator.Send(validRequestStub, CancellationToken.None)).ReturnsAsync(catalogItemDtoMock);

        var actual = await _catalogItemsController.GetCatalogItemAsync(validRequestStub, CancellationToken.None);

        actual.Result.Should().BeOfType<OkObjectResult>();
        ((actual.Result as OkObjectResult)!.Value as CatalogItemDto)!.Id.Should().Be(catalogItemDtoMock.Id);
        ((actual.Result as OkObjectResult)!.Value as CatalogItemDto)!.Name.Should().Be(catalogItemDtoMock.Name);
    }

    [Fact]
    public async Task GetCatalogItemAsync_WhenQueryIsValidAndProductDoesntExist_ThenReturnsNotFoundResult()
    {
        var validProductIdStub = Guid.NewGuid();
        var validRequestStub = new GetById.Query(validProductIdStub);
        CatalogItemDto invalidCatalogItemDtoStub = null!;
        _mediatorStub.Setup(mediator => mediator.Send(validRequestStub, CancellationToken.None)).ReturnsAsync(invalidCatalogItemDtoStub);

        var actual = await _catalogItemsController.GetCatalogItemAsync(validRequestStub, CancellationToken.None);

        actual.Result.Should().BeOfType<NotFoundResult>();
    }

    // GetCatalogItemsAsync Tests

    [Fact]
    public async Task GetCatalogItemsAsync_WhenQueryIsValidAndProductsExist_ThenReturnsOkObjectResult()
    {
        var firstIdStub = Guid.NewGuid();
        var secondIdStub = Guid.NewGuid();
        var validQueryStub = CatalogItemFakes.GetGetAllQueryFake(firstIdStub, secondIdStub);
        var itemsMock = CatalogItemFakes.GetCatalogItemDtosFake(firstIdStub, secondIdStub);
        var paginatedDtoStub = CatalogItemFakes.GetPaginatedDtoFake(itemsMock);
        _mediatorStub.Setup(mediator => mediator.Send(validQueryStub, CancellationToken.None)).ReturnsAsync(paginatedDtoStub);

        var actual = await _catalogItemsController.GetCatalogItemsAsync(validQueryStub, CancellationToken.None);

        actual.Result.Should().BeOfType<OkObjectResult>();
        ((actual.Result as OkObjectResult)!.Value as PaginatedDto<CatalogItemDto>)!.Count.Should().Be(2);
        ((actual.Result as OkObjectResult)!.Value as PaginatedDto<CatalogItemDto>)!.Items.Should().BeEquivalentTo(itemsMock);
    }

    // GetCatalogItemsByTypeAndBrandAsync Tests

    [Fact]
    public async Task GetCatalogItemsByTypeAndBrandAsync_WhenQueryIsValidAndProductsExist_ThenReturnsOkObjectResult()
    {
        var typeIdStub = Guid.NewGuid();
        var brandIdStub = Guid.NewGuid();
        var validQueryStub = CatalogItemFakes.GetByTypeAndBrandFake(typeIdStub, brandIdStub);
        var itemsMock = CatalogItemFakes.GetCatalogItemDtosFake(typeIdStub, brandIdStub);
        var paginatedDtoStub = CatalogItemFakes.GetPaginatedDtoFake(itemsMock);
        _mediatorStub.Setup(mediator => mediator.Send(validQueryStub, CancellationToken.None)).ReturnsAsync(paginatedDtoStub);

        var actual = await _catalogItemsController.GetCatalogItemsByTypeAndBrandAsync(validQueryStub, CancellationToken.None);

        actual.Result.Should().BeOfType<OkObjectResult>();
        ((actual.Result as OkObjectResult)!.Value as PaginatedDto<CatalogItemDto>)!.Count.Should().Be(2);
        ((actual.Result as OkObjectResult)!.Value as PaginatedDto<CatalogItemDto>)!.Items.Should().BeEquivalentTo(itemsMock);
    }
}