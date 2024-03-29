namespace Catalog.API.Features.CatalogItems;

[ApiController]
[ApiVersion("1.0")]
// [Route("api/catalog")]
[Route("api/v{version:apiVersion}/catalog")]
public class CatalogItemsController : ControllerBase
{
    private const string IdErrorMessage = "'{{{0}}}' must be valid and equal to the command id.";
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
    public async Task<IActionResult> CreateCatalogItemAsync([FromBody] Create.Command command)
    {
        try
        {
            Guid id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetCatalogItemAsync), new { id = id }, null);
        }
        catch (Exception ex) when (ex is ValidationException or ArgumentNullException)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT api/v1/[controller]/items/3fa85f64-5717-4562-b3fc-2c963f66afa6
    [HttpPut("items/{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCatalogItemAsync([FromRoute] Guid id, [FromBody] Update.Command command)
    {
        try
        {
            if (id.Equals(Guid.Empty) || !id.Equals(command?.Id))
            {
                throw new ValidationException(string.Format(IdErrorMessage, nameof(id)));
            }

            bool Updated = await _mediator.Send(command);

            return Updated ? NoContent() : NotFound();
        }
        catch (Exception ex) when (ex is ValidationException or ArgumentNullException)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE api/v1/[controller]/items/3fa85f64-5717-4562-b3fc-2c963f66afa6
    [HttpDelete("items/{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCatalogItemAsync([FromRoute] Delete.Command command)
    {
        try
        {
            bool deleted = await _mediator.Send(command);

            return deleted ? NoContent() : NotFound();
        }
        catch (Exception ex) when (ex is ValidationException or ArgumentNullException)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET api/v1/[controller]/items/3fa85f64-5717-4562-b3fc-2c963f66afa6
    [HttpGet("items/{id:Guid}")]
    [ActionName(nameof(GetCatalogItemAsync))]
    [ProducesResponseType(typeof(CatalogItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CatalogItemDto>> GetCatalogItemAsync([FromRoute] GetById.Query query, CancellationToken cancellationToken)
    {
        try
        {
            CatalogItemDto? item = await _mediator.Send(query, cancellationToken);

            return item is null ? NotFound() : Ok(item);
        }
        catch (Exception ex) when (ex is ValidationException or ArgumentNullException)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET api/v1/[controller]/items?Ids=6f11c1cc-42ff-4bfc-904d-2c5c7e5b546a%3B8781d5ba-071b-4ab7-b6f6-1bc732594b31&PageIndex=0&PageSize=10
    [HttpGet("items")]
    [ActionName(nameof(GetCatalogItemsAsync))]
    [ProducesResponseType(typeof(PaginatedCollection<CatalogItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PaginatedCollection<CatalogItemDto>>> GetCatalogItemsAsync([FromQuery] GetAll.Query query, CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await _mediator.Send(query, cancellationToken));
        }
        catch (Exception ex) when (ex is ValidationException or ArgumentNullException)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET api/v1/[controller]/items/type/32488b09-fdfc-4fa0-affc-daee7d017818/brand/96c7905b-7a9b-4b7e-b479-6b307ab0d5fa?PageIndex=0&PageSize=10
    [HttpGet("items/type/{CatalogTypeId:Guid}/brand/{CatalogBrandId:Guid?}")]
    [ActionName(nameof(GetCatalogItemsByTypeAndBrandAsync))]
    [ProducesResponseType(typeof(PaginatedCollection<CatalogItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PaginatedCollection<CatalogItemDto>>> GetCatalogItemsByTypeAndBrandAsync([FromRoute] GetByTypeAndBrand.Query query, CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await _mediator.Send(query, cancellationToken));
        }
        catch (Exception ex) when (ex is ValidationException or ArgumentNullException)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET api/v1/[controller]/items/brand/96c7905b-7a9b-4b7e-b479-6b307ab0d5fa?PageIndex=0&PageSize=10
    [HttpGet("items/brand/{CatalogBrandId:Guid}")]
    [ActionName(nameof(GetCatalogItemsByBrandAsync))]
    [ProducesResponseType(typeof(PaginatedCollection<CatalogItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PaginatedCollection<CatalogItemDto>>> GetCatalogItemsByBrandAsync([FromRoute] GetByBrand.Query query, CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await _mediator.Send(query, cancellationToken));
        }
        catch (Exception ex) when (ex is ValidationException or ArgumentNullException)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET api/v1/[controller]/items/name/cup?PageIndex=0&PageSize=10
    [HttpGet("items/name/{Name}")]
    [ActionName(nameof(GetCatalogItemsByNameAsync))]
    [ProducesResponseType(typeof(PaginatedCollection<CatalogItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PaginatedCollection<CatalogItemDto>>> GetCatalogItemsByNameAsync([FromRoute] GetByName.Query query, CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await _mediator.Send(query, cancellationToken));
        }
        catch (Exception ex) when (ex is ValidationException or ArgumentNullException)
        {
            return BadRequest(ex.Message);
        }
    }
}
