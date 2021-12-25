namespace Catalog.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
// [Route("api/catalog")]
[Route("api/v{version:apiVersion}/catalog")]
public class CatalogTypesController : ControllerBase
{
    private readonly ILogger<CatalogTypesController> _logger;
    private readonly IMediator _mediator;

    public CatalogTypesController(ILogger<CatalogTypesController> logger, IMediator mediator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    // GET api/v1/[controller]/types
    [HttpGet("types")]
    [ActionName(nameof(GetCatalogTypesAsync))]
    [ProducesResponseType(typeof(IReadOnlyCollection<CatalogTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyCollection<CatalogTypeDto>>> GetCatalogTypesAsync(CancellationToken cancellationToken)
    => Ok(await _mediator.Send(new GetAllTypes.Query(), cancellationToken));
}
