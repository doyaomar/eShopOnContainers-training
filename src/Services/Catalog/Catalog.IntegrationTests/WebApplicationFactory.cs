namespace Catalog.IntegrationTests;

public class WebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var runner = MongoDbRunner.Start();
        MongoClient client = new(runner.ConnectionString);

        builder
        .ConfigureTestServices(services =>
        {
            services.AddSingleton<IMongoClient>(_ => client);
        });
    }
}