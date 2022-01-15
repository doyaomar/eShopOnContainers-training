namespace Catalog.IntegrationTests;

public class WebApplicationFactory : WebApplicationFactory<Program>, IDisposable
{
    private MongoDbRunner _runner = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _runner = MongoDbRunner.Start();
        Debug.WriteLine($"MongoDbRunner.ConnectionString ::: {_runner.ConnectionString}");
        _runner.Import("CatalogDb", "catalogItems", Path.Combine("Data", "catalogItems.json"), true);
        _runner.Import("CatalogDb", "catalogTypes", Path.Combine("Data", "catalogTypes.json"), true);
        _runner.Import("CatalogDb", "catalogBrands", Path.Combine("Data", "catalogBrands.json"), true);

        builder
        .ConfigureServices(services =>
        {
            services.AddSingleton<IMongoClient>(_ => new MongoClient(_runner.ConnectionString));
        });
    }

    void IDisposable.Dispose()
    {
        _runner.Dispose();
        base.Dispose();
    }
}