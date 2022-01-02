namespace Catalog.IntegrationTests.Features.CatalogTypes;

public class CatalogTypesControllerTests : IClassFixture<WebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory _factory;

    public CatalogTypesControllerTests(WebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    // CatalogTypesController Tests

    [Fact]
    public async Task GetCatalogTypesAsync_WhenTypesExists_ThenReturnsOkStatusCode()
    {
        var url = "api/v1/catalog/types";

        var actual = await _client.GetAsync(url);

        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}