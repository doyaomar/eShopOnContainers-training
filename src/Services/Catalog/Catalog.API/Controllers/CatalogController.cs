using AutoMapper;
using Catalog.API.Dtos;
using Catalog.API.Models;
using Catalog.API.Requests;
using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

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
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CatalogItemDto>> GetProductAsync([FromRoute] long id)
    {
        if (id < 1)
        {
            return BadRequest();
        }

        CatalogItem? product = await _catalogService.GetProductAsync(id);

        return product is null
        ? NotFound()
        : Ok(_mapper.Map<CatalogItemDto>(product));
    }

    // POST api/v1/[controller]/products
    [HttpPost("products")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductRequest request)
    {
        if (request is null)
        {
            return BadRequest();
        }

        var product = _mapper.Map<CatalogItem>(request);
        CatalogItem? createdProduct = await _catalogService.CreateProductAsync(product);

        return createdProduct is null
        ? StatusCode(StatusCodes.Status500InternalServerError)
        : CreatedAtAction(nameof(GetProductAsync), new { id = createdProduct.Id }, null);
    }

    // PUT api/v1/[controller]/products/5
    [HttpPut("products/{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> UpdateProductAsync([FromRoute] long id, [FromBody] UpdateProductRequest request)
    {
        if (id < 1 || request is null || !id.Equals(request.Id))
        {
            return BadRequest();
        }

        var productToUpdate = _mapper.Map<CatalogItem>(request);
        CatalogItem? updatedProduct = await _catalogService.UpdateProductAsync(productToUpdate);

        return updatedProduct is null
        ? NotFound()
        : NoContent();
    }

    // DELETE api/v1/[controller]/products/3
    [HttpDelete("products/{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> DeleteProductAsync([FromRoute] long id)
    {
        if (id < 1)
        {
            return BadRequest();
        }

        CatalogItem? deletedProduct = await _catalogService.DeleteProductAsync(id);

        return deletedProduct is null
        ? NotFound()
        : NoContent();
    }
}
