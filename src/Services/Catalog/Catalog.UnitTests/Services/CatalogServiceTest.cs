using System.Threading.Tasks;
using Xunit;
using Moq;
using Catalog.API.Infrastructure;
using Catalog.API.Services;
using Catalog.UnitTests.Fakes;
using FluentAssertions;
using Catalog.API.Models;

namespace Catalog.UnitTests.Services;

public class CatalogServiceTest
{
    private readonly Mock<ICatalogRepository> _catalogRepositoryStub;
    private readonly ICatalogService _catalogService;

    public CatalogServiceTest()
    {
        _catalogRepositoryStub = new Mock<ICatalogRepository>();

        _catalogService = new CatalogService(_catalogRepositoryStub.Object);
    }

    // GetProductAsync Tests

    [Fact]
    public async Task GetProductAsync_WhenProductExists_ThenReturnsProduct()
    {
        // Arrange
        var validProductIdStub = 1;
        var asNoTrackingStub = false;
        var validCatalogItemMock = CatalogItemFake.GetCatalogItemFake();
        _catalogRepositoryStub.Setup(repo => repo.GetAsync(validProductIdStub, asNoTrackingStub)).ReturnsAsync(validCatalogItemMock);

        // Act
        var result = await _catalogService.GetProductAsync(validProductIdStub, asNoTrackingStub);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(validCatalogItemMock.Id);
        result.Name.Should().Be(validCatalogItemMock.Name);
    }

    [Fact]
    public async Task GetProductAsync_WhenProductDoesntExist_ThenReturnsNull()
    {
        // Arrange
        var validProductIdStub = 1;
        var asNoTrackingStub = false;
        CatalogItem invalidCatalogItemstub = null;
        _catalogRepositoryStub.Setup(repo => repo.GetAsync(validProductIdStub, asNoTrackingStub)).ReturnsAsync(invalidCatalogItemstub);

        // Act
        var result = await _catalogService.GetProductAsync(validProductIdStub, asNoTrackingStub);

        // Assert
        result.Should().BeNull();
    }

    // CreateProductAsync Tests

    [Fact]
    public async Task CreateProductAsync_WhenCatalogItemIsValid_ThenReturnsCreatedProduct()
    {
        // Arrange
        var validCatalogItemMock = CatalogItemFake.GetCatalogItemFake();
        _catalogRepositoryStub.Setup(repo => repo.CreateAsync(validCatalogItemMock)).ReturnsAsync(validCatalogItemMock);

        // Act
        var result = await _catalogService.CreateProductAsync(validCatalogItemMock);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(validCatalogItemMock.Id);
        result.Name.Should().Be(validCatalogItemMock.Name);
    }

    // UpdateProductAsync Tests

    [Fact]
    public async Task UpdateProductAsync_WhenCatalogItemIsValid_ThenSuccess()
    {
        // Arrange
        var validCatalogItemStub = CatalogItemFake.GetCatalogItemFake();

        // Act
        await _catalogService.UpdateProductAsync(validCatalogItemStub);

        // Assert
        _catalogRepositoryStub.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }

    // DeleteProductAsync Tests
    
    [Fact]
    public void DeleteProductAsync()
    {
        // Arrange
        var validCatalogItemStub = CatalogItemFake.GetCatalogItemFake();

        // Act
        _catalogService.DeleteProductAsync(validCatalogItemStub);

        // Assert
        _catalogRepositoryStub.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }
}