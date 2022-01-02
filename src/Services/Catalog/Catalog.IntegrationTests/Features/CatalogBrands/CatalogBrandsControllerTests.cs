namespace Catalog.IntegrationTests.Features.CatalogBrands;

public class CatalogBrandsControllerTests : IClassFixture<WebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory _factory;

    public CatalogBrandsControllerTests(WebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    // CatalogBrandsController Tests

    [Fact]
    public async Task GetCatalogBrandsAsync_WhenBrandsExists_ThenReturnsOkStatusCode()
    {
        var url = "api/v1/catalog/brands";

        var actual = await _client.GetAsync(url);

        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}