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
    public async Task<IActionResult> CreateCatalogItemAsync([FromBody] Create.Command request)
    {
        Guid id = await _mediator.Send(request);

        return CreatedAtAction(nameof(GetCatalogItemAsync), new { id = id }, null);
    }

    // PUT api/v1/[controller]/items/3fa85f64-5717-4562-b3fc-2c963f66afa6
    [HttpPut("items/{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCatalogItemAsync([FromRoute] Guid id, [FromBody] Update.Command request)
    {
        if (id.Equals(Guid.Empty) || !id.Equals(request?.Id))
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
    public async Task<IActionResult> DeleteCatalogItemAsync([FromRoute] Delete.Command request)
    {
        bool deleted = await _mediator.Send(request);

        return deleted ? NoContent() : NotFound();
    }

    // GET api/v1/[controller]/items/3fa85f64-5717-4562-b3fc-2c963f66afa6
    [HttpGet("items/{id:Guid}")]
    [ActionName(nameof(GetCatalogItemAsync))]
    [ProducesResponseType(typeof(CatalogItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CatalogItemDto>> GetCatalogItemAsync([FromRoute] GetById.Query request, CancellationToken cancellationToken)
    {
        CatalogItemDto? item = await _mediator.Send(request, cancellationToken);

        return item is null ? NotFound() : Ok(item);
    }

    // GET api/v1/[controller]/items?Ids=6f11c1cc-42ff-4bfc-904d-2c5c7e5b546a%3B8781d5ba-071b-4ab7-b6f6-1bc732594b31&PageIndex=0&PageSize=10
    [HttpGet("items")]
    [ActionName(nameof(GetCatalogItemsAsync))]
    [ProducesResponseType(typeof(PaginatedDto<CatalogItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PaginatedDto<CatalogItemDto>>> GetCatalogItemsAsync([FromQuery] GetAll.Query request, CancellationToken cancellationToken)
    => Ok(await _mediator.Send(request, cancellationToken));
}
