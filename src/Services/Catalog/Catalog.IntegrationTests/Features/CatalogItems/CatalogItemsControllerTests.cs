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
    public async Task GetCatalogItemAsync_WhenQueryIsValidAndProductExists_ThenReturnsOkStatusCode()
    {
        var url = "api/v1/catalog/items/6f11c1cc-42ff-4bfc-904d-2c5c7e5b546a";

        var actual = await _client.GetAsync(url);

        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetCatalogItemAsync_WhenQueryIsValidAndProductExists_ThenReturnsNotFoundStatusCode()
    {
        var url = "api/v1/catalog/items/04637f10-c262-4a8a-8e27-5acca985e04f";

        var actual = await _client.GetAsync(url);

        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // DeleteCatalogItemAsync Tests

    [Fact]
    public async Task DeleteCatalogItemAsync_WhenCommandIsValidAndProductExists_ThenReturnsNoContentStatusCode()
    {
        var url = "api/v1/catalog/items/6bca9cf2-25dc-447d-bf8e-fb4323ae865d";

        var actual = await _client.DeleteAsync(url);

        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteCatalogItemAsync_WhenCommandIsValidAndProductExists_ThenReturnsNotFoundStatusCode()
    {
        var url = "api/v1/catalog/items/04637f10-c262-4a8a-8e27-5acca985e04f";

        var actual = await _client.DeleteAsync(url);

        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // CreateCatalogItemAsync Tests

    [Fact]
    public async Task CreateCatalogItemAsync_WhenCommandIsValid_ThenReturnsCreatedStatusCode()
    {
        var url = "api/v1/catalog/items";
        var command = new
        {
            name = "name",
            description = "description",
            price = 10,
            pictureFileName = "fileName",
            catalogBrand = new
            {
                id = Guid.NewGuid(),
                name = "catalogBrandName"
            },
            catalogType = new
            {
                id = Guid.NewGuid(),
                name = "catalogTypeName"
            },
            availableStock = 23,
            restockThreshold = 30,
            maxStockThreshold = 30,
            isOnReorder = false
        };
        var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

        var actual = await _client.PostAsync(url, content);

        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    // UpdateCatalogItemAsync Tests

    [Fact]
    public async Task UpdateCatalogItemAsync_WhenCommandIsValidAndProductExists_ThenReturnsNoContentStatusCode()
    {
        var url = "api/v1/catalog/items/3e44cdcb-02c3-4112-a89b-f69e9b343680";
        var command = new
        {
            id = "3e44cdcb-02c3-4112-a89b-f69e9b343680",
            name = "name",
            description = "description",
            price = 10,
            pictureFileName = "fileName",
            catalogBrand = new
            {
                id = Guid.NewGuid(),
                name = "catalogBrandName"
            },
            catalogType = new
            {
                id = Guid.NewGuid(),
                name = "catalogTypeName"
            },
            availableStock = 23,
            restockThreshold = 30,
            maxStockThreshold = 30,
            isOnReorder = false
        };
        var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

        var actual = await _client.PutAsync(url, content);

        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task UpdateCatalogItemAsync_WhenCommandIsValidAndProductExists_ThenReturnsNotFoundStatusCode()
    {
        var url = "api/v1/catalog/items/04637f10-c262-4a8a-8e27-5acca985e04f";
        var command = new
        {
            id = "04637f10-c262-4a8a-8e27-5acca985e04f",
            name = "name",
            description = "description",
            price = 10,
            pictureFileName = "fileName",
            catalogBrand = new
            {
                id = Guid.NewGuid(),
                name = "catalogBrandName"
            },
            catalogType = new
            {
                id = Guid.NewGuid(),
                name = "catalogTypeName"
            },
            availableStock = 23,
            restockThreshold = 30,
            maxStockThreshold = 30,
            isOnReorder = false
        };
        var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

        var actual = await _client.PutAsync(url, content);

        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // GetCatalogItemsAsync Tests

    [Fact]
    public async Task GetCatalogItemsAsync_WhenQueryIsValidAndProductsExist_ThenReturnsOkStatusCode()
    {
        var url =
        "api/v1/catalog/items?Ids=6f11c1cc-42ff-4bfc-904d-2c5c7e5b546a;46202c09-3dd5-4928-a728-f43ac4d3ee32&PageIndex=0&PageSize=5";

        var actual = await _client.GetAsync(url);

        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        var stringContent = await actual.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var paginatedItems = JsonSerializer.Deserialize<PaginatedDto<CatalogItemDto>>(stringContent, options);
        paginatedItems!.Count.Should().Be(2);
    }

    // GetCatalogItemsByTypeAndBrandAsync Tests

    [Fact]
    public async Task GetCatalogItemsByTypeAndBrandAsync_WhenQueryIsValidAndProductsExist_ThenReturnsOkStatusCode()
    {
        var url =
        "api/v1/catalog/items/type/32488b09-fdfc-4fa0-affc-daee7d017818/brand/5f9516ec-1533-4b7b-adf3-876d8bd5c085?PageIndex=0&PageSize=5";

        var actual = await _client.GetAsync(url);

        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        var stringContent = await actual.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var paginatedItems = JsonSerializer.Deserialize<PaginatedDto<CatalogItemDto>>(stringContent, options);
        paginatedItems!.Count.Should().Be(2);
    }

    // GetCatalogItemsByBrandAsync Tests

    [Fact]
    public async Task GetCatalogItemsByBrandAsync_WhenQueryIsValidAndProductsExist_ThenReturnsOkStatusCode()
    {
        var url =
        "api/v1/catalog/items/brand/5f9516ec-1533-4b7b-adf3-876d8bd5c085?PageIndex=0&PageSize=5";

        var actual = await _client.GetAsync(url);

        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        var stringContent = await actual.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var paginatedItems = JsonSerializer.Deserialize<PaginatedDto<CatalogItemDto>>(stringContent, options);
        paginatedItems!.Count.Should().Be(7);
    }
    // GetCatalogItemsByNameAsync Tests

    [Fact]
    public async Task GetCatalogItemsByNameAsync_WhenQueryIsValidAndProductsExist_ThenReturnsOkStatusCode()
    {
        var url =
        "api/v1/catalog/items/name/.NET";

        var actual = await _client.GetAsync(url);

        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        var stringContent = await actual.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var paginatedItems = JsonSerializer.Deserialize<PaginatedDto<CatalogItemDto>>(stringContent, options);
        paginatedItems!.Count.Should().Be(5);
    }
}