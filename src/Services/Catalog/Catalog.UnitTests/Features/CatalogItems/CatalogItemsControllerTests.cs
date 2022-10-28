namespace Catalog.UnitTests.Features.CatalogItems;

public class CatalogItemsControllerTests
{
    private readonly CatalogItemsController _catalogItemsController;
    private readonly Mock<IMediator> _mediatorStub;

    public CatalogItemsControllerTests()
    {
        _mediatorStub = new();
        Mock<ILogger<CatalogItemsController>> logger = new();
        _catalogItemsController = new(logger.Object, _mediatorStub.Object);
    }

    // CreateProductAsync Tests

    [Fact]
    public async Task CreateProductAsync_WhenCreateCommandtIsValidAndProductExists_ThenReturnsCreatedAtActionResult()
    {
        var validCommandtStub = CatalogItemFakes.GetCreateCommandFake();
        var validProductIdMock = Guid.NewGuid();
        _mediatorStub.Setup(mediator => mediator.Send(validCommandtStub, CancellationToken.None)).ReturnsAsync(validProductIdMock);

        var actual = await _catalogItemsController.CreateCatalogItemAsync(validCommandtStub);

        actual.Should().BeOfType<CreatedAtActionResult>();
        (actual as CreatedAtActionResult)!.ActionName.Should().Be("GetCatalogItemAsync");
        (actual as CreatedAtActionResult)!.RouteValues!.Values.Should().Contain(validProductIdMock);
    }

    [Fact]
    public async Task CreateProductAsync_WhenCreateCommandtIsNotValid_ThenReturnsBadRequestObjectResult()
    {
        var invalidCommandtStub = CatalogItemFakes.GetCreateCommandInvalidFake();
        _mediatorStub.Setup(mediator => mediator.Send(invalidCommandtStub, CancellationToken.None)).Throws(new ValidationException("Validation Error"));

        var actual = await _catalogItemsController.CreateCatalogItemAsync(invalidCommandtStub);

        actual.Should().BeOfType<BadRequestObjectResult>();
    }

    // UpdateCatalogItemAsync Tests

    [Fact]
    public async Task UpdateCatalogItemAsync_WhenUpdateCommandtIsValidAndProductExists_ThenReturnsNoContentResult()
    {
        var validProductIdStub = Guid.NewGuid();
        var validCommandtStub = CatalogItemFakes.GetUpdateCommandFake(validProductIdStub);
        var updatedStub = true;
        _mediatorStub.Setup(mediator => mediator.Send(validCommandtStub, CancellationToken.None)).ReturnsAsync(updatedStub);

        var actual = await _catalogItemsController.UpdateCatalogItemAsync(validProductIdStub, validCommandtStub);

        actual.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task UpdateCatalogItemAsync_WhenUpdateCommandtIsValidAndProductDoesntExist_ThenReturnsNotFoundResult()
    {
        var validProductIdStub = Guid.NewGuid();
        var validCommandtStub = CatalogItemFakes.GetUpdateCommandFake(validProductIdStub);
        var updatedStub = false;
        _mediatorStub.Setup(mediator => mediator.Send(validCommandtStub, CancellationToken.None)).ReturnsAsync(updatedStub);

        var actual = await _catalogItemsController.UpdateCatalogItemAsync(validProductIdStub, validCommandtStub);

        actual.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task UpdateCatalogItemAsync_WhenUpdateCommandtIsNotValid_ThenReturnsBadRequestResult()
    {
        var validProductIdStub = Guid.NewGuid();
        var invalidCommandtStub = CatalogItemFakes.GetUpdateCommandFake(Guid.NewGuid());

        var actual = await _catalogItemsController.UpdateCatalogItemAsync(validProductIdStub, invalidCommandtStub);

        actual.Should().BeOfType<BadRequestResult>();
    }

    // DeleteCatalogItemAsync Tests

    [Fact]
    public async Task DeleteCatalogItemAsync_WhenDeleteCommandtIsValidAndProductExists_ThenReturnsNoContentResult()
    {
        var validProductIdStub = Guid.NewGuid();
        var validCommandtStub = new Delete.Command(validProductIdStub);
        var deletedStub = true;
        _mediatorStub.Setup(mediator => mediator.Send(validCommandtStub, CancellationToken.None)).ReturnsAsync(deletedStub);

        var actual = await _catalogItemsController.DeleteCatalogItemAsync(validCommandtStub);

        actual.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task DeleteCatalogItemAsync_WhenDeleteCommandtIsNotValid_ThenReturnsBadRequestObjectResult()
    {
        var invalidProductIdStub = Guid.Empty;
        var invalidCommandtStub = new Delete.Command(invalidProductIdStub);
        _mediatorStub.Setup(mediator => mediator.Send(invalidCommandtStub, CancellationToken.None)).Throws(new ValidationException("Validation Error"));

        var actual = await _catalogItemsController.DeleteCatalogItemAsync(invalidCommandtStub);

        actual.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task DeleteCatalogItemAsync_WhenDeleteCommandIsNull_ThenReturnsBadRequestObjectResult()
    {
        var invalidProductIdStub = Guid.Empty;
        var invalidCommandtStub = new Delete.Command(invalidProductIdStub);
        _mediatorStub.Setup(mediator => mediator.Send(invalidCommandtStub, CancellationToken.None)).Throws(new ArgumentNullException(nameof(Delete.Command)));

        var actual = await _catalogItemsController.DeleteCatalogItemAsync(invalidCommandtStub);

        actual.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task DeleteCatalogItemAsync_WhenDeleteCommandtIsValidAndProductDoesntExist_ThenReturnsNotFoundResult()
    {
        var validProductIdStub = Guid.NewGuid();
        var validCommandtStub = new Delete.Command(validProductIdStub);
        var deletedStub = false;
        _mediatorStub.Setup(mediator => mediator.Send(validCommandtStub, CancellationToken.None)).ReturnsAsync(deletedStub);

        var actual = await _catalogItemsController.DeleteCatalogItemAsync(validCommandtStub);

        actual.Should().BeOfType<NotFoundResult>();
    }

    // GetCatalogItemAsync Tests

    [Fact]
    public async Task GetCatalogItemAsync_WhenQueryIsValidAndProductExists_ThenReturnsOkObjectResult()
    {
        var validProductIdStub = Guid.NewGuid();
        var validCommandtStub = new GetById.Query(validProductIdStub);
        var catalogItemDtoMock = CatalogItemFakes.GetCatalogItemDtoFake(validProductIdStub);
        _mediatorStub.Setup(mediator => mediator.Send(validCommandtStub, CancellationToken.None)).ReturnsAsync(catalogItemDtoMock);

        var actual = await _catalogItemsController.GetCatalogItemAsync(validCommandtStub, CancellationToken.None);

        actual.Result.Should().BeOfType<OkObjectResult>();
        ((actual.Result as OkObjectResult)!.Value as CatalogItemDto)!.Id.Should().Be(catalogItemDtoMock.Id);
        ((actual.Result as OkObjectResult)!.Value as CatalogItemDto)!.Name.Should().Be(catalogItemDtoMock.Name);
    }

    [Fact]
    public async Task GetCatalogItemAsync_WhenQueryIsValidAndProductDoesntExist_ThenReturnsNotFoundResult()
    {
        var validProductIdStub = Guid.NewGuid();
        var validCommandtStub = new GetById.Query(validProductIdStub);
        CatalogItemDto invalidCatalogItemDtoStub = null!;
        _mediatorStub.Setup(mediator => mediator.Send(validCommandtStub, CancellationToken.None)).ReturnsAsync(invalidCatalogItemDtoStub);

        var actual = await _catalogItemsController.GetCatalogItemAsync(validCommandtStub, CancellationToken.None);

        actual.Result.Should().BeOfType<NotFoundResult>();
    }

    // GetCatalogItemsAsync Tests

    [Fact]
    public async Task GetCatalogItemsAsync_WhenQueryIsValidAndProductsExist_ThenReturnsOkObjectResult()
    {
        var idsStub = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var validQueryStub = CatalogItemFakes.GetGetAllQueryFake(string.Join(';', idsStub));
        var itemsMock = CatalogItemFakes.GetCatalogItemDtosFake(idsStub);
        var paginatedDtoStub = CatalogItemFakes.GetPaginatedDtoFake(itemsMock);
        _mediatorStub.Setup(mediator => mediator.Send(validQueryStub, CancellationToken.None)).ReturnsAsync(paginatedDtoStub);

        var actual = await _catalogItemsController.GetCatalogItemsAsync(validQueryStub, CancellationToken.None);

        actual.Result.Should().BeOfType<OkObjectResult>();
        ((actual.Result as OkObjectResult)!.Value as PaginatedCollection<CatalogItemDto>)!.Count.Should().Be(2);
        ((actual.Result as OkObjectResult)!.Value as PaginatedCollection<CatalogItemDto>)!.Items.Should().Equal(itemsMock);
    }

    // GetCatalogItemsByTypeAndBrandAsync Tests

    [Fact]
    public async Task GetCatalogItemsByTypeAndBrandAsync_WhenQueryIsValidAndProductsExist_ThenReturnsOkObjectResult()
    {
        var validQueryStub = CatalogItemFakes.GetByTypeAndBrandQueryFake(Guid.NewGuid(), Guid.NewGuid());
        var itemsMock = CatalogItemFakes.GetCatalogItemDtosFake(new List<Guid> { Guid.NewGuid(), Guid.NewGuid() });
        var paginatedDtoStub = CatalogItemFakes.GetPaginatedDtoFake(itemsMock);
        _mediatorStub.Setup(mediator => mediator.Send(validQueryStub, CancellationToken.None)).ReturnsAsync(paginatedDtoStub);

        var actual = await _catalogItemsController.GetCatalogItemsByTypeAndBrandAsync(validQueryStub, CancellationToken.None);

        actual.Result.Should().BeOfType<OkObjectResult>();
        ((actual.Result as OkObjectResult)!.Value as PaginatedCollection<CatalogItemDto>)!.Count.Should().Be(2);
        ((actual.Result as OkObjectResult)!.Value as PaginatedCollection<CatalogItemDto>)!.Items.Should().Equal(itemsMock);
    }

    // GetCatalogItemsByBrandAsync Tests

    [Fact]
    public async Task GetCatalogItemsByBrandAsync_WhenQueryIsValidAndProductsExist_ThenReturnsOkObjectResult()
    {
        var validQueryStub = CatalogItemFakes.GetByBrandQueryFake(Guid.NewGuid());
        var itemsMock = CatalogItemFakes.GetCatalogItemDtosFake(new List<Guid> { Guid.NewGuid(), Guid.NewGuid() });
        var paginatedDtoStub = CatalogItemFakes.GetPaginatedDtoFake(itemsMock);
        _mediatorStub.Setup(mediator => mediator.Send(validQueryStub, CancellationToken.None)).ReturnsAsync(paginatedDtoStub);

        var actual = await _catalogItemsController.GetCatalogItemsByBrandAsync(validQueryStub, CancellationToken.None);

        actual.Result.Should().BeOfType<OkObjectResult>();
        ((actual.Result as OkObjectResult)!.Value as PaginatedCollection<CatalogItemDto>)!.Count.Should().Be(2);
        ((actual.Result as OkObjectResult)!.Value as PaginatedCollection<CatalogItemDto>)!.Items.Should().Equal(itemsMock);
    }

    // GetCatalogItemsByNameAsync Tests

    [Fact]
    public async Task GetCatalogItemsByNameAsync_WhenQueryIsValidAndProductsExist_ThenReturnsOkObjectResult()
    {
        var validQueryStub = CatalogItemFakes.GetByNameQueryFake("name");
        var itemsMock = CatalogItemFakes.GetCatalogItemDtosFake(new List<Guid> { Guid.NewGuid(), Guid.NewGuid() });
        var paginatedDtoStub = CatalogItemFakes.GetPaginatedDtoFake(itemsMock);
        _mediatorStub.Setup(mediator => mediator.Send(validQueryStub, CancellationToken.None)).ReturnsAsync(paginatedDtoStub);

        var actual = await _catalogItemsController.GetCatalogItemsByNameAsync(validQueryStub, CancellationToken.None);

        actual.Result.Should().BeOfType<OkObjectResult>();
        ((actual.Result as OkObjectResult)!.Value as PaginatedCollection<CatalogItemDto>)!.Count.Should().Be(2);
        ((actual.Result as OkObjectResult)!.Value as PaginatedCollection<CatalogItemDto>)!.Items.Should().Equal(itemsMock);
    }
}