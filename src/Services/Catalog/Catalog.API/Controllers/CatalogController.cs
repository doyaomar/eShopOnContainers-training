using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.API.Dtos;
using Catalog.API.Models;
using Catalog.API.Requests;
using Catalog.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ILogger<CatalogController> _logger;
        private readonly IMapper _mapper;
        private readonly ICatalogService _catalogService;

        public CatalogController(ILogger<CatalogController> logger, IMapper mapper, ICatalogService catalogService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        }

        // GET api/v1/[controller]/products/4
        [HttpGet("products/{id:long}")]
        [ActionName(nameof(GetProductAsync))]
        [ProducesResponseType(typeof(CatalogItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CatalogItemDto>> GetProductAsync([FromRoute] long id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            CatalogItem product = await _catalogService.GetProductAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            return _mapper.Map<CatalogItemDto>(product);
        }

        // POST api/v1/[controller]/products
        [HttpPost("products")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateProductAsync([FromBody] CreateProductRequest request)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var product = _mapper.Map<CatalogItem>(request);

            CatalogItem createdProduct = await _catalogService.CreateProductAsync(product);

            return CreatedAtAction(nameof(GetProductAsync), new { id = createdProduct.Id }, null);
        }

        // PUT api/v1/[controller]/products/5
        [HttpPut("products/{id:long}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProductAsync([FromRoute] long id, [FromBody] UpdateProductRequest request)
        {
            if (request is null || id < 1 || !id.Equals(request.Id))
            {
                return BadRequest();
            }

            CatalogItem productToUpdate = await _catalogService.GetProductAsync(id, true);

            if (productToUpdate is null)
            {
                return NotFound();
            }

            productToUpdate = _mapper.Map<CatalogItem>(request);

            await _catalogService.UpdateProductAsync(productToUpdate);

            return CreatedAtAction(nameof(GetProductAsync), new { id = id }, null);
        }

        // DELETE api/v1/[controller]/products/3
        [HttpDelete("products/{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] long id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            CatalogItem productToDelete = await _catalogService.GetProductAsync(id);

            if (productToDelete is null)
            {
                return NotFound();
            }

            await _catalogService.DeleteProductAsync(productToDelete);

            return NoContent();
        }
    }
}