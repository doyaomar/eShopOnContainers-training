namespace Catalog.UnitTests.Controllers;

public class CatalogControllerTest
{
    private readonly CatalogController _catalogController;
    private readonly Mock<ICatalogService> _catalogServiceStub;
    private readonly Mock<IMapper> _mapperStub;

    public CatalogControllerTest()
    {
        Mock<ILogger<CatalogController>> _loggerStub = new();
        _catalogServiceStub = new Mock<ICatalogService>();
        _mapperStub = new Mock<IMapper>();
        _catalogController = new CatalogController(_loggerStub.Object, _mapperStub.Object, _catalogServiceStub.Object);
    }

    // GetProductAsync Tests

    [Fact]
    public async Task GetProductAsync_WhenProductExists_ReturnsItemDto()
    {
        // Arrange
        var validProductIdStub = Guid.NewGuid();
        var validCatalogItemStub = CatalogItemFake.GetCatalogItemFake();
        var validCatalogItemDtoMock = CatalogItemFake.GetCatalogItemDtoFake(validProductIdStub);
        _catalogServiceStub.Setup(service => service.GetProductAsync(validProductIdStub)).ReturnsAsync(validCatalogItemStub);
        _mapperStub.Setup(mapper => mapper.Map<CatalogItemDto>(validCatalogItemStub)).Returns(validCatalogItemDtoMock);

        // Act
        var result = await _catalogController.GetProductAsync(validProductIdStub);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        ((result.Result as OkObjectResult)!.Value as CatalogItemDto)!.Id.Should().Be(validCatalogItemDtoMock.Id);
        ((result.Result as OkObjectResult)!.Value as CatalogItemDto)!.Name.Should().Be(validCatalogItemDtoMock.Name);
    }

    [Fact]
    public async Task GetProductAsync_WhenIdIsNotValid_ReturnsBadRequestResult()
    {
        // Arrange
        var invalidProductIdStub = Guid.Empty;

        // Act
        var result = await _catalogController.GetProductAsync(invalidProductIdStub);

        // Assert
        result.Result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task GetProductAsync_WhenProductDoesntExist_ReturnsNotFoundResult()
    {
        // Arrange
        var invalidProductIdStub = Guid.NewGuid();
        CatalogItem invalidCatalogItemStub = null!;
        _catalogServiceStub.Setup(service => service.GetProductAsync(invalidProductIdStub)).ReturnsAsync(invalidCatalogItemStub);


        // Act
        var result = await _catalogController.GetProductAsync(invalidProductIdStub);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    // CreateProductAsync Tests

    [Fact]
    public async Task CreateProductAsync_WhenCreateRequestIsValidAndProductExists_ReturnsCreatedResult()
    {
        // Arrange
        var validRequestStub = CatalogItemFake.GetCreateProductRequestFake();
        var catalogItemStub = CatalogItemFake.GetCatalogItemFake();
        _mapperStub.Setup(mapper => mapper.Map<CatalogItem>(validRequestStub)).Returns(catalogItemStub);
        _catalogServiceStub.Setup(service => service.CreateProductAsync(catalogItemStub)).ReturnsAsync(catalogItemStub);

        // Act
        var result = await _catalogController.CreateProductAsync(validRequestStub);

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>();
    }

    // UpdateProductAsync Tests

    [Fact]
    public async Task UpdateProductAsync_WhenUpdateRequestIsValidAndProductExists_ReturnsNoContentResult()
    {
        // Arrange
        var validProductIdStub = Guid.NewGuid(); ;
        var validRequestStub = CatalogItemFake.GetUpdateProductRequestFake(validProductIdStub);
        var validCatalogItemStub = CatalogItemFake.GetCatalogItemFake();
        _mapperStub.Setup(mapper => mapper.Map<CatalogItem>(validRequestStub)).Returns(validCatalogItemStub);
        _catalogServiceStub.Setup(service => service.UpdateProductAsync(validCatalogItemStub)).ReturnsAsync(validCatalogItemStub);

        // Act
        var result = await _catalogController.UpdateProductAsync(validProductIdStub, validRequestStub);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task UpdateProductAsync_WhenProductDoesntExist_ReturnsNotFountResult()
    {
        // Arrange
        var validProductIdStub = Guid.NewGuid(); ;
        var validRequestStub = CatalogItemFake.GetUpdateProductRequestFake(validProductIdStub);
        var validCatalogItemStub = CatalogItemFake.GetCatalogItemFake();
        _mapperStub.Setup(mapper => mapper.Map<CatalogItem>(validRequestStub)).Returns(validCatalogItemStub);
        _catalogServiceStub.Setup(service => service.UpdateProductAsync(validCatalogItemStub)).ReturnsAsync((CatalogItem)null!);


        // Act
        var result = await _catalogController.UpdateProductAsync(validProductIdStub, validRequestStub);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task UpdateProductAsync_WhenIdIsNotValid_ReturnsBadRequestResult()
    {
        // Arrange
        var invalidProductIdStub = Guid.Empty;
        var validRequestStub = CatalogItemFake.GetUpdateProductRequestFake(invalidProductIdStub);

        // Act
        var result = await _catalogController.UpdateProductAsync(invalidProductIdStub, validRequestStub);

        // Assert
        result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task UpdateProductAsync_WhenIdIsDifferentThanRequest_ReturnsBadRequestResult()
    {
        // Arrange
        var invalidProductIdStub = Guid.NewGuid();
        var validRequestStub = CatalogItemFake.GetUpdateProductRequestFake(Guid.NewGuid());

        // Act
        var result = await _catalogController.UpdateProductAsync(invalidProductIdStub, validRequestStub);

        // Assert
        result.Should().BeOfType<BadRequestResult>();
    }

    // DeleteProductAsync Tests

    [Fact]
    public async Task DeleteProductAsync_WhenIdIsValidAndProductExists_ReturnsNoContentResult()
    {
        // Arrange
        var validProductIdStub = Guid.NewGuid();
        var validCatalogItemStub = CatalogItemFake.GetCatalogItemFake();
        _catalogServiceStub.Setup(service => service.DeleteProductAsync(validProductIdStub)).ReturnsAsync(validCatalogItemStub);

        // Act
        var result = await _catalogController.DeleteProductAsync(validProductIdStub);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task DeleteProductAsync_WhenProductDoesntExist_ReturnsNotFountResult()
    {
        // Arrange
        var validProductIdStub = Guid.NewGuid();
        CatalogItem invalidCatalogItemStub = null!;
        _catalogServiceStub.Setup(service => service.DeleteProductAsync(validProductIdStub)).ReturnsAsync(invalidCatalogItemStub);


        // Act
        var result = await _catalogController.DeleteProductAsync(validProductIdStub);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task DeleteProductAsync_WhenIdIsNotValid_ReturnsBadRequestResult()
    {
        // Arrange
        var invalidProductIdStub = Guid.Empty;

        // Act
        var result = await _catalogController.DeleteProductAsync(invalidProductIdStub);

        // Assert
        result.Should().BeOfType<BadRequestResult>();
    }
}
