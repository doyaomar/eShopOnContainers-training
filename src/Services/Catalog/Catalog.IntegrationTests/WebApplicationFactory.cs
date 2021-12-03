namespace Catalog.IntegrationTests;

public class WebApplicationFactory : WebApplicationFactory<Program>
{

    private MongoDbRunner _runner = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _runner = MongoDbRunner.Start();
        _runner.Import("CatalogDb", "catalogItems", @"Data\catalogItems.json", false);

        builder
        .ConfigureTestServices(services =>
        {
            services.AddSingleton<IMongoClient>(_ => new MongoClient(_runner.ConnectionString));
        });
    }

    public override ValueTask DisposeAsync()
    {
        _runner.Dispose();
        return base.DisposeAsync();
    }
}