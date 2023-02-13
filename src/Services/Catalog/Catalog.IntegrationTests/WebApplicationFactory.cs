namespace Catalog.IntegrationTests;

public class WebApplicationFactory : WebApplicationFactory<Program>, IDisposable
{
    private IMongoRunner _runner = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _runner = MongoRunner.Run();
        var database = new MongoClient(_runner.ConnectionString).GetDatabase("CatalogDb");
        Debug.WriteLine($"MongoDbRunner.ConnectionString ::: {_runner.ConnectionString}");

        database.CreateCollection("catalogItems");
        database.CreateCollection("catalogTypes");
        database.CreateCollection("catalogBrands");

        // Import a collection. Full method signature:
        _runner.Import("CatalogDb", "catalogItems", Path.Combine("Data", "catalogItems.json"), null, true);
        _runner.Import("CatalogDb", "catalogTypes", Path.Combine("Data", "catalogTypes.json"), null, true);
        _runner.Import("CatalogDb", "catalogBrands", Path.Combine("Data", "catalogBrands.json"), null, true);

        builder
        .ConfigureServices(services =>
        {
            services.AddSingleton<IMongoClient>(_ => new MongoClient(_runner.ConnectionString));
        });

        builder.UseEnvironment("Development");
    }

    void IDisposable.Dispose()
    {
        _runner.Dispose();
        base.Dispose();
    }
}