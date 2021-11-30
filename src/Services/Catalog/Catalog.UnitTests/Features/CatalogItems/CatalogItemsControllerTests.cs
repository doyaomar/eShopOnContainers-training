namespace Catalog.UnitTests.Features.CatalogItems;

public class CatalogItemsControllerTests
{
    readonly CatalogItemsController _catalogItemsController;
    readonly Mock<IMediator> _mediatorStub;

    public CatalogItemsControllerTests()
    {
        _mediatorStub = new Mock<IMediator>();
        Mock<ILogger<CatalogItemsController>> logger = new();
        _catalogItemsController = new CatalogItemsController(logger.Object, _mediatorStub.Object);
    }

    // CreateProductAsync Tests

    [Fact]
    public async Task CreateProductAsync_WhenCreateRequestIsValidAndProductExists_ThenReturnsCreatedAtActionResult()
    {
        var validRequestStub = CatalogItemFakes.GetCreateCommandFake();
        var validProductIdMock = Guid.NewGuid();
        var validCancellationTokenStub = CancellationToken.None;
        _mediatorStub.Setup(mediator => mediator.Send(validRequestStub, validCancellationTokenStub)).ReturnsAsync(validProductIdMock);

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
        var validCancellationTokenStub = CancellationToken.None;
        var updatedStub = true;
        _mediatorStub.Setup(mediator => mediator.Send(validRequestStub, validCancellationTokenStub)).ReturnsAsync(updatedStub);

        var actual = await _catalogItemsController.UpdateCatalogItemAsync(validProductIdStub, validRequestStub);

        actual.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task UpdateCatalogItemAsync_WhenUpdateRequestIsValidAndProductDoesntExist_ThenReturnsNotFoundResult()
    {
        var validProductIdStub = Guid.NewGuid();
        var validRequestStub = CatalogItemFakes.GetUpdateCommandFake(validProductIdStub);
        var validCancellationTokenStub = CancellationToken.None;
        var updatedStub = false;
        _mediatorStub.Setup(mediator => mediator.Send(validRequestStub, validCancellationTokenStub)).ReturnsAsync(updatedStub);

        var actual = await _catalogItemsController.UpdateCatalogItemAsync(validProductIdStub, validRequestStub);

        actual.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task UpdateCatalogItemAsync_WhenUpdateRequestIsNotValid_ThenReturnsBadRequestResult()
    {
        var validProductIdStub = Guid.NewGuid();
        var invalidRequestStub = CatalogItemFakes.GetUpdateCommandFake(Guid.NewGuid());
        var validCancellationTokenStub = CancellationToken.None;

        var actual = await _catalogItemsController.UpdateCatalogItemAsync(validProductIdStub, invalidRequestStub);

        actual.Should().BeOfType<BadRequestResult>();
    }

    // DeleteCatalogItemAsync Tests

    [Fact]
    public async Task DeleteCatalogItemAsync_WhenDeleteRequestIsValidAndProductExists_ThenReturnsNoContentResult()
    {
        var validProductIdStub = Guid.NewGuid();
        var validRequestStub = new Delete.Command(validProductIdStub);
        var validCancellationTokenStub = CancellationToken.None;
        var deletedStub = true;
        _mediatorStub.Setup(mediator => mediator.Send(validRequestStub, validCancellationTokenStub)).ReturnsAsync(deletedStub);

        var actual = await _catalogItemsController.DeleteCatalogItemAsync(validRequestStub);

        actual.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task DeleteCatalogItemAsync_WhenDeleteRequestIsValidAndProductDoesntExist_ThenReturnsNotFoundResult()
    {
        var validProductIdStub = Guid.NewGuid();
        var validRequestStub = new Delete.Command(validProductIdStub);
        var validCancellationTokenStub = CancellationToken.None;
        var deletedStub = false;
        _mediatorStub.Setup(mediator => mediator.Send(validRequestStub, validCancellationTokenStub)).ReturnsAsync(deletedStub);

        var actual = await _catalogItemsController.DeleteCatalogItemAsync(validRequestStub);

        actual.Should().BeOfType<NotFoundResult>();
    }

    // GetCatalogItemAsync Tests

    [Fact]
    public async Task GetCatalogItemAsync_WhenQueryIsValidAndProductExists_ThenReturnsCatalogItem()
    {
        var validProductIdStub = Guid.NewGuid();
        var validRequestStub = new GetById.Query(validProductIdStub);
        var catalogItemDtoMock = CatalogItemFakes.GetCatalogItemDtoFake(validProductIdStub);
        var validCancellationTokenStub = CancellationToken.None;
        _mediatorStub.Setup(mediator => mediator.Send(validRequestStub, validCancellationTokenStub)).ReturnsAsync(catalogItemDtoMock);

        var actual = await _catalogItemsController.GetCatalogItemAsync(validRequestStub, validCancellationTokenStub);

        actual.Result.Should().BeOfType<OkObjectResult>();
        ((actual.Result as OkObjectResult)!.Value as CatalogItemDto)!.Id.Should().Be(catalogItemDtoMock.Id);
        ((actual.Result as OkObjectResult)!.Value as CatalogItemDto)!.Name.Should().Be(catalogItemDtoMock.Name);
    }

    [Fact]
    public async Task GetCatalogItemAsync_WhenQueryIsValidAndProductDoesntExist_ThenReturnsCatalogItem()
    {
        var validProductIdStub = Guid.NewGuid();
        var validRequestStub = new GetById.Query(validProductIdStub);
        CatalogItemDto invalidCatalogItemDtoStub = null!;
        var validCancellationTokenStub = CancellationToken.None;
        _mediatorStub.Setup(mediator => mediator.Send(validRequestStub, validCancellationTokenStub)).ReturnsAsync(invalidCatalogItemDtoStub);

        var actual = await _catalogItemsController.GetCatalogItemAsync(validRequestStub, validCancellationTokenStub);

        actual.Result.Should().BeOfType<NotFoundResult>();
    }
}