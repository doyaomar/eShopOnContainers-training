namespace Catalog.API.Features.CatalogPictures;

[ApiController]
[ApiVersion("1.0")]
// [Route("api/catalog")]
[Route("api/v{version:apiVersion}/catalog")]
public class CatalogPicturesController : ControllerBase
{
    private readonly ILogger<CatalogPicturesController> _logger;
    private readonly IMediator _mediator;

    public CatalogPicturesController(ILogger<CatalogPicturesController> logger, IMediator mediator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("items/{id:Guid}/picture")]
    [ActionName(nameof(DownloadCatalogItemPictureAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DownloadCatalogItemPictureAsync([FromRoute] DownloadPicture.Query query, CancellationToken cancellationToken)
    {
        var picture = await _mediator.Send(query, cancellationToken);

        return picture is null ? NotFound() : File(picture.Buffer, picture.ContentType);
    }

    [HttpPost("items/{id:Guid}/picture")]
    [ActionName(nameof(UploadCatalogItemPictureAsync))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UploadCatalogItemPictureAsync(UploadPicture.Command command, CancellationToken cancellationToken)
    {
        var uploaded = await _mediator.Send(command, cancellationToken);

        return uploaded ? CreatedAtAction(nameof(DownloadCatalogItemPictureAsync), new { id = command.Id }, null) : NotFound();
    }
}
