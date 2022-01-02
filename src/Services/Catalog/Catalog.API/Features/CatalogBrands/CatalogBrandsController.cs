namespace Catalog.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
// [Route("api/catalog")]
[Route("api/v{version:apiVersion}/catalog")]
public class CatalogBrandsController : ControllerBase
{
    private readonly ILogger<CatalogBrandsController> _logger;

    private readonly IMediator _mediator;

    public CatalogBrandsController(ILogger<CatalogBrandsController> logger, IMediator mediator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    // GET api/v1/[controller]/brands
    [HttpGet("brands")]
    [ActionName(nameof(GetCatalogBrandsAsync))]
    [ProducesResponseType(typeof(IReadOnlyCollection<CatalogBrandDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyCollection<CatalogBrandDto>>> GetCatalogBrandsAsync(CancellationToken cancellationToken)
    => Ok(await _mediator.Send(new GetAllBrands.Query(), cancellationToken));
}
