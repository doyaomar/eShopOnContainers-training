using Xunit;
using Catalog.API.Controllers;
using Catalog.API.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Catalog.UnitTests.Fakes;
using Microsoft.AspNetCore.Mvc;
using Catalog.API.Dtos;
using Catalog.API.Models;
using Catalog.API.Requests;

namespace Catalog.UnitTests.Controllers
{
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
            var validProductIdStub = 1;
            var catalogItemStub = CatalogItemFake.GetCatalogItemFake();
            var catalogItemDtoMock = CatalogItemFake.GetCatalogItemDtoFake();
            _catalogServiceStub.Setup(service => service.GetProductAsync(validProductIdStub, It.IsAny<bool>())).ReturnsAsync(catalogItemStub);
            _mapperStub.Setup(mapper => mapper.Map<CatalogItemDto>(It.IsAny<CatalogItem>())).Returns(catalogItemDtoMock);

            // Act
            var result = await _catalogController.GetProductAsync(validProductIdStub);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            ((result.Result as OkObjectResult).Value as CatalogItemDto).Id.Should().Be(catalogItemDtoMock.Id);
            ((result.Result as OkObjectResult).Value as CatalogItemDto).Name.Should().Be(catalogItemDtoMock.Name);
        }

        [Fact]
        public async Task GetProductAsync_WhenIdIsNotValid_ReturnsBadRequestResult()
        {
            // Arrange
            var invalidProductIdStub = 0;

            // Act
            var result = await _catalogController.GetProductAsync(invalidProductIdStub);

            // Assert
            result.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task GetProductAsync_WhenProductDoesntExist_ReturnsNotFoundResult()
        {
            // Arrange
            var invalidProductIdStub = 1;
            CatalogItem catalogItemStub = null;
            _catalogServiceStub.Setup(service => service.GetProductAsync(It.IsAny<long>(), It.IsAny<bool>())).ReturnsAsync(catalogItemStub);


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

        [Fact]
        public async Task CreateProductAsync_WhenCreateRequestIsNull_ReturnsBadRequestResult()
        {
            // Arrange
            CreateProductRequest invalidRequestStub = null;

            // Act
            var result = await _catalogController.CreateProductAsync(invalidRequestStub);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        // UpdateProductAsync Tests

        [Fact]
        public async Task UpdateProductAsync_WhenUpdateRequestIsValidAndProductExists_ReturnsNoContentResult()
        {
            // Arrange
            var validProductIdStub = 1;
            var validRequestStub = CatalogItemFake.GetUpdateProductRequestFake();
            var catalogItemStub = CatalogItemFake.GetCatalogItemFake();
            _mapperStub.Setup(mapper => mapper.Map<CatalogItem>(validRequestStub)).Returns(catalogItemStub);
            _catalogServiceStub.Setup(service => service.GetProductAsync(validProductIdStub, It.IsAny<bool>())).ReturnsAsync(catalogItemStub);
            _catalogServiceStub.Setup(service => service.UpdateProductAsync(catalogItemStub));

            // Act
            var result = await _catalogController.UpdateProductAsync(validProductIdStub, validRequestStub);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task UpdateProductAsync_WhenProductDoesntExist_ReturnsNotFountResult()
        {
            // Arrange
            var validProductIdStub = 1;
            var validRequestStub = CatalogItemFake.GetUpdateProductRequestFake();
            var catalogItemStub = CatalogItemFake.GetCatalogItemFake();
            _mapperStub.Setup(mapper => mapper.Map<CatalogItem>(validRequestStub)).Returns(catalogItemStub);
            _catalogServiceStub.Setup(service => service.GetProductAsync(validProductIdStub, It.IsAny<bool>())).ReturnsAsync((CatalogItem)null);

            // Act
            var result = await _catalogController.UpdateProductAsync(validProductIdStub, validRequestStub);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task UpdateProductAsync_WhenIdIsNotValid_ReturnsBadRequestResult()
        {
            // Arrange
            var invalidProductIdStub = 0;
            var validRequestStub = CatalogItemFake.GetUpdateProductRequestFake();

            // Act
            var result = await _catalogController.UpdateProductAsync(invalidProductIdStub, validRequestStub);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateProductAsync_WhenIdIsDifferentThanRequest_ReturnsBadRequestResult()
        {
            // Arrange
            var invalidProductIdStub = 2;
            var validRequestStub = CatalogItemFake.GetUpdateProductRequestFake();

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
            var validProductIdStub = 1;
            var catalogItemStub = CatalogItemFake.GetCatalogItemFake();
            _catalogServiceStub.Setup(service => service.GetProductAsync(validProductIdStub, It.IsAny<bool>())).ReturnsAsync(catalogItemStub);
            _catalogServiceStub.Setup(service => service.DeleteProductAsync(catalogItemStub));

            // Act
            var result = await _catalogController.DeleteProductAsync(validProductIdStub);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteProductAsync_WhenProductDoesntExist_ReturnsNotFountResult()
        {
            // Arrange
            var validProductIdStub = 1;
            CatalogItem catalogItemStub = null;
            _catalogServiceStub.Setup(service => service.GetProductAsync(validProductIdStub, It.IsAny<bool>())).ReturnsAsync(catalogItemStub);

            // Act
            var result = await _catalogController.DeleteProductAsync(validProductIdStub);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeleteProductAsync_WhenIdIsNotValid_ReturnsBadRequestResult()
        {
            // Arrange
            var invalidProductIdStub = 0;

            // Act
            var result = await _catalogController.DeleteProductAsync(invalidProductIdStub);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}