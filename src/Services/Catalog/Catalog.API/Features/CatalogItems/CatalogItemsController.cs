using Catalog.API.Features.CatalogItems;

namespace Catalog.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/catalog")]
public class CatalogItemsController : ControllerBase
{
    private readonly ILogger<CatalogItemsController> _logger;
    private readonly IMediator _mediator;

    public CatalogItemsController(ILogger<CatalogItemsController> logger, IMediator mediator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    // POST api/v1/[controller]/items
    [HttpPost("items")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateProductAsync([FromBody] Create.Command request)
    {
        Guid id = await _mediator.Send(request);

        return CreatedAtAction(nameof(GetProductAsync), new { id = id }, null);
    }

    // PUT api/v1/[controller]/items/3fa85f64-5717-4562-b3fc-2c963f66afa6
    [HttpPut("items/{id:Guid}")]
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

        bool Updated = await _mediator.Send(request);

        return Updated ? NoContent() : NotFound();
    }

    // DELETE api/v1/[controller]/items/3fa85f64-5717-4562-b3fc-2c963f66afa6
    [HttpDelete("items/{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteProductAsync([FromRoute] Delete.Command request)
    {
        bool deleted = await _mediator.Send(request);

        return deleted ? NoContent() : NotFound();
    }

    // GET api/v1/[controller]/items/3fa85f64-5717-4562-b3fc-2c963f66afa6
    [HttpGet("items/{id:Guid}")]
    [ActionName(nameof(GetProductAsync))]
    [ProducesResponseType(typeof(CatalogItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CatalogItemDto>> GetProductAsync([FromRoute] GetById.Query request, CancellationToken cancellationToken)
    {
        CatalogItemDto? item = await _mediator.Send(request, cancellationToken);

        return item is null ? NotFound() : Ok(item);
    }
}
