using System.Threading.Tasks;
using Xunit;
using Moq;
using Catalog.API.Infrastructure;
using Catalog.API.Services;
using Catalog.UnitTests.Fakes;
using FluentAssertions;
using Catalog.API.Models;
using Catalog.API.SeedWork;
using System;

namespace Catalog.UnitTests.Services;

public class CatalogServiceTest
{
    private readonly Mock<ICatalogDbContext> _catalogRepositoryStub;
    private readonly Mock<IGuidProvider> _guidProviderStub;
    private readonly ICatalogService _catalogService;

    public CatalogServiceTest()
    {
        _catalogRepositoryStub = new Mock<ICatalogDbContext>();
        _guidProviderStub = new Mock<IGuidProvider>();
        _catalogService = new CatalogService(_catalogRepositoryStub.Object, _guidProviderStub.Object);
    }

    // GetProductAsync Tests

    [Fact]
    public async Task GetProductAsync_WhenProductExists_ThenReturnsProduct()
    {
        // Arrange
        var validProductIdStub = Guid.NewGuid();
        var validCatalogItemMock = CatalogItemFake.GetCatalogItemFake();
        _catalogRepositoryStub.Setup(repo => repo.GetAsync(validProductIdStub)).ReturnsAsync(validCatalogItemMock);

        // Act
        var result = await _catalogService.GetProductAsync(validProductIdStub);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(validCatalogItemMock.Id);
        result!.Name.Should().Be(validCatalogItemMock.Name);
    }

    [Fact]
    public async Task GetProductAsync_WhenProductDoesntExist_ThenReturnsNull()
    {
        // Arrange
        var validProductIdStub = Guid.NewGuid();
        CatalogItem invalidCatalogItemstub = null!;
        _catalogRepositoryStub.Setup(repo => repo.GetAsync(validProductIdStub)).ReturnsAsync(invalidCatalogItemstub);

        // Act
        var result = await _catalogService.GetProductAsync(validProductIdStub);

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
        result!.Id.Should().Be(validCatalogItemMock.Id);
        result.Name.Should().Be(validCatalogItemMock.Name);
    }

    // UpdateProductAsync Tests

    [Fact]
    public async Task UpdateProductAsync_WhenCatalogItemIsValid_ThenSuccess()
    {
        // Arrange
        var validCatalogItemStub = CatalogItemFake.GetCatalogItemFake();
        _catalogRepositoryStub.Setup(repo => repo.UpdateAsync(validCatalogItemStub)).ReturnsAsync(validCatalogItemStub);


        // Act
        var result = await _catalogService.UpdateProductAsync(validCatalogItemStub);

        // Assert
        result.Should().NotBeNull();
    }

    // DeleteProductAsync Tests

    [Fact]
    public void DeleteProductAsync()
    {
        // Arrange
        var validProductIdStub = Guid.NewGuid();
        var validCatalogItemStub = CatalogItemFake.GetCatalogItemFake();
        _catalogRepositoryStub.Setup(repo => repo.DeleteAsync(validProductIdStub)).ReturnsAsync(validCatalogItemStub);


        // Act
        var result = _catalogService.DeleteProductAsync(validProductIdStub);

        // Assert
        result.Should().NotBeNull();
    }
}