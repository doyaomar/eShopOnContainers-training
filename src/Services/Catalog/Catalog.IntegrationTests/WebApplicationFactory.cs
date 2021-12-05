namespace Catalog.IntegrationTests;

public class WebApplicationFactory : WebApplicationFactory<Program>
{

    private MongoDbRunner _runner = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _runner = MongoDbRunner.Start();
        Debug.WriteLine($"MongoDbRunner.ConnectionString ::: {_runner.ConnectionString}");
        string[] paths = { "Data", "catalogItems.json" };
        _runner.Import("CatalogDb", "catalogItems", Path.Combine(paths), true);

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