using Catalog.API.Features.CatalogItems;

namespace Catalog.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[Controller]")]
public class CatalogItemsController : ControllerBase
{
    private readonly ILogger<CatalogItemsController> _logger;
    private readonly IMediator _mediator;

    public CatalogItemsController(ILogger<CatalogItemsController> logger, IMediator mediator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    // // GET api/v1/[controller]/products/3fa85f64-5717-4562-b3fc-2c963f66afa6
    // [HttpGet("products/{id:Guid}")]
    // [ActionName(nameof(GetProductAsync))]
    // [ProducesResponseType(typeof(CatalogItemViewModel), StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // public async Task<ActionResult<CatalogItemViewModel>> GetProductAsync([FromRoute] Guid id)
    // {
    //     if (id.Equals(Guid.Empty))
    //     {
    //         return BadRequest();
    //     }

    //     CatalogItem? product = await _catalogService.GetProductAsync(id);

    //     return product is null
    //     ? NotFound()
    //     : Ok(_mapper.Map<CatalogItemViewModel>(product));
    // }

    // POST api/v1/[controller]/products
    [HttpPost("products")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> CreateProductAsync([FromBody] Create.Command request)
    {
        Guid id = await _mediator.Send(request);

        return CreatedAtAction("", id, null);
        // return CreatedAtAction(nameof(GetProductAsync), id, null);
    }

    // PUT api/v1/[controller]/products/3fa85f64-5717-4562-b3fc-2c963f66afa6
    [HttpPut("products/{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> UpdateProductAsync([FromRoute] Guid id, [FromBody] Update.Command request)
    {
        if (id.Equals(Guid.Empty) || request is null || !id.Equals(request.Id))
        {
            return BadRequest();
        }
        
        var Updated = await _mediator.Send(request);

        return Updated
        ? NoContent()
        : NotFound();
    }

    // // DELETE api/v1/[controller]/products/3fa85f64-5717-4562-b3fc-2c963f66afa6
    // [HttpDelete("products/{id:Guid}")]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    // public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid id)
    // {
    //     if (id.Equals(Guid.Empty))
    //     {
    //         return BadRequest();
    //     }

    //     CatalogItem? deletedProduct = await _catalogService.DeleteProductAsync(id);

    //     return deletedProduct is null
    //     ? NotFound()
    //     : NoContent();
    // }
}
