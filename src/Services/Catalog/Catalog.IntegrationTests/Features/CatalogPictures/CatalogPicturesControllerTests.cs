namespace Catalog.IntegrationTests.Features.CatalogPictures;

[WebApplicationFactoryContentRootAttribute("Program","","","")]
public class CatalogPicturesControllerTests : IClassFixture<WebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory _factory;

    public CatalogPicturesControllerTests(WebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    // DownloadCatalogItemPictureAsync Tests

    [Fact]
    public async Task DownloadCatalogItemPictureAsync_WhenQueryIsValidAndPictureExists_ThenReturnsOkStatusCode()
    {
        var url = "api/v1/catalog/items/6f11c1cc-42ff-4bfc-904d-2c5c7e5b546a/picture";

        var actual = await _client.GetAsync(url);

        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}