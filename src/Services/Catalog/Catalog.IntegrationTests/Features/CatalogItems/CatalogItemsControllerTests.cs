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

    [Fact]
    public async Task GetCatalogItemAsync_WhenQueryIsValidAndProductExists_ThenReturnsSuccesStatusCode()
    {
        var url = "api/v1/catalog/items/6f11c1cc-42ff-4bfc-904d-2c5c7e5b546a";

        var actual = await _client.GetAsync(url);

        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}