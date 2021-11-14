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
        private readonly Mock<ICatalogService> _stubCatalogService;
        private readonly Mock<IMapper> _stubMapper;
        private readonly Mock<ILogger<CatalogController>> _stubLogger;

        public CatalogControllerTest()
        {
            _stubCatalogService = new Mock<ICatalogService>();
            _stubLogger = new Mock<ILogger<CatalogController>>();
            _stubMapper = new Mock<IMapper>();

            _catalogController = new CatalogController(_stubLogger.Object, _stubMapper.Object, _stubCatalogService.Object);
        }

        // GetProductAsync tests
        [Fact]
        public async Task GetProductAsync_WhenCalalogItemExist_ReturnsItemDto()
        {
            // Arrange
            var validProductId = 1;
            var mockCatalogItem = CatalogItemFake.GetCatalogItemFake();
            var mockCatalogItemDto = CatalogItemFake.GetCatalogItemDtoFake();
            _stubCatalogService.Setup(service => service.GetProductAsync(It.IsAny<long>(), It.IsAny<bool>())).ReturnsAsync(mockCatalogItem);
            _stubMapper.Setup(mapper => mapper.Map<CatalogItemDto>(It.IsAny<CatalogItem>())).Returns(mockCatalogItemDto);

            // Act
            var result = await _catalogController.GetProductAsync(validProductId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            ((result.Result as OkObjectResult).Value as CatalogItemDto).Id.Should().Be(mockCatalogItem.Id);
            ((result.Result as OkObjectResult).Value as CatalogItemDto).Name.Should().Be(mockCatalogItem.Name);
        }

        [Fact]
        public async Task GetProductAsync_WhenIdIsNotValid_ReturnsBadRequestResult()
        {
            // Arrange
            var invalidProductId = 0;

            // Act
            var result = await _catalogController.GetProductAsync(invalidProductId);

            // Assert
            result.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task GetProductAsync_WhenProductDoesntExist_ReturnsNotFoundResult()
        {
            // Arrange
            var invalidProductId = 1;
            _stubCatalogService.Setup(service => service.GetProductAsync(It.IsAny<long>(), It.IsAny<bool>())).ReturnsAsync((CatalogItem)null);


            // Act
            var result = await _catalogController.GetProductAsync(invalidProductId);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        // CreateProductAsync tests
        [Fact]
        public async Task CreateProductAsync_WhenCreateRequestIsValid_ReturnsCreatedResult()
        {
            // Arrange
            var mockRequest = CatalogItemFake.GetCreateProductRequestFake();
            var mockCatalogItem = CatalogItemFake.GetCatalogItemFake();
            _stubMapper.Setup(mapper => mapper.Map<CatalogItem>(mockRequest)).Returns(mockCatalogItem);
            _stubCatalogService.Setup(service => service.CreateProductAsync(mockCatalogItem)).ReturnsAsync(mockCatalogItem);

            // Act
            var result = await _catalogController.CreateProductAsync(mockRequest);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task CreateProductAsync_WhenCreateRequestIsNull_ReturnsBadRequestResult()
        {
            // Arrange
            CreateProductRequest mockRequest = null;

            // Act
            var result = await _catalogController.CreateProductAsync(mockRequest);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}