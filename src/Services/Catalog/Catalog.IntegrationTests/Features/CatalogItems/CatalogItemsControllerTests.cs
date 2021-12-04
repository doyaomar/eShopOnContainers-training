namespace Catalog.IntegrationTests.Features.CatalogItems;

public class CatalogItemsControllerTests : IClassFixture<WebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory _factory;

    public CatalogItemsControllerTests(WebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    // GetCatalogItemAsync Tests

    [Fact]
    public async Task GetCatalogItemAsync_WhenQueryIsValidAndProductExists_ThenReturnsOkResult()
    {
        var url = "api/v1/catalog/items/6f11c1cc-42ff-4bfc-904d-2c5c7e5b546a";

        var actual = await _client.GetAsync(url);

        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetCatalogItemAsync_WhenQueryIsValidAndProductExists_ThenReturnsNotFoundResult()
    {
        var url = "api/v1/catalog/items/04637f10-c262-4a8a-8e27-5acca985e04f";

        var actual = await _client.GetAsync(url);

        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // DeleteCatalogItemAsync Tests

    [Fact]
    public async Task DeleteCatalogItemAsync_WhenCommandIsValidAndProductExists_ThenReturnsNoContentResult()
    {
        var url = "api/v1/catalog/items/6bca9cf2-25dc-447d-bf8e-fb4323ae865d";

        var actual = await _client.DeleteAsync(url);

        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteCatalogItemAsync_WhenCommandIsValidAndProductExists_ThenReturnsNotFoundResult()
    {
        var url = "api/v1/catalog/items/04637f10-c262-4a8a-8e27-5acca985e04f";

        var actual = await _client.DeleteAsync(url);

        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}