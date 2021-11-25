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

    // GET api/v1/[controller]/products/3fa85f64-5717-4562-b3fc-2c963f66afa6
    [HttpGet("products/{id:Guid}")]
    [ActionName(nameof(GetProductAsync))]
    [ProducesResponseType(typeof(CatalogItemViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CatalogItemViewModel>> GetProductAsync([FromRoute] Guid id)
    {
        if (id.Equals(Guid.Empty))
        {
            return BadRequest();
        }

        CatalogItem? product = await _catalogService.GetProductAsync(id);

        return product is null
        ? NotFound()
        : Ok(_mapper.Map<CatalogItemViewModel>(product));
    }

    // POST api/v1/[controller]/products
    [HttpPost("products")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductRequest request)
    {
        var product = _mapper.Map<CatalogItem>(request);
        CatalogItem createdProduct = await _catalogService.CreateProductAsync(product);

        return CreatedAtAction(nameof(GetProductAsync), new { id = createdProduct.Id }, null);
    }

    // PUT api/v1/[controller]/products/3fa85f64-5717-4562-b3fc-2c963f66afa6
    [HttpPut("products/{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> UpdateProductAsync([FromRoute] Guid id, [FromBody] UpdateProductRequest request)
    {
        if (id.Equals(Guid.Empty) || request is null || !id.Equals(request.Id))
        {
            return BadRequest();
        }

        var productToUpdate = _mapper.Map<CatalogItem>(request);
        CatalogItem? updatedProduct = await _catalogService.UpdateProductAsync(productToUpdate);

        return updatedProduct is null
        ? NotFound()
        : NoContent();
    }

    // DELETE api/v1/[controller]/products/3fa85f64-5717-4562-b3fc-2c963f66afa6
    [HttpDelete("products/{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid id)
    {
        if (id.Equals(Guid.Empty))
        {
            return BadRequest();
        }

        CatalogItem? deletedProduct = await _catalogService.DeleteProductAsync(id);

        return deletedProduct is null
        ? NotFound()
        : NoContent();
    }
}
