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

        [Fact]
        public async Task GetProductAsync_WhenCalalogItemExist_ReturnsItemDto()
        {
            // Arrange
            var mockCatalogItem = CatalogItemFake.CreateCatalogItemFake();
            var mockCatalogItemDto = CatalogItemFake.CreateCatalogItemDtoFake();
            _stubCatalogService.Setup(service => service.GetProductAsync(It.IsAny<long>(),It.IsAny<bool>())).ReturnsAsync(mockCatalogItem);
            _stubMapper.Setup(mapper => mapper.Map<CatalogItemDto>(It.IsAny<CatalogItem>())).Returns(mockCatalogItemDto);

            // Act
            var result = await _catalogController.GetProductAsync(1) as ActionResult<CatalogItemDto>;

            // Assert
            //result.Should().BeOfType<OkObjectResult>();            
            result.Value.Id.Should().Be(mockCatalogItem.Id);
            result.Value.Name.Should().Be(mockCatalogItem.Name);
        }

        [Fact]
        public async Task GetProductAsync_WhenIdIsNotValid_ReturnsBadRequestResult()
        {
            // Act
            var result = await _catalogController.GetProductAsync(0) as ActionResult<CatalogItemDto>;;

            // Assert
            result.Result.Should().BeOfType<BadRequestResult>();
        }
    }
}