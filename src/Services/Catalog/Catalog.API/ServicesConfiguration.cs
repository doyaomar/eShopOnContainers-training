namespace Catalog.API;

public static class ServicesConfiguration
{
    private const string catalogDbConnectionString = "CatalogDb";
    private const string checkName = "self";

    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
        .AddScoped<ICatalogService, CatalogService>()
        .AddScoped<IGuidService, GuidService>()
        .AddScoped<CatalogDbContext>()
        .AddSingleton<IMongoClient>(_ => new MongoClient(configuration.GetConnectionString(catalogDbConnectionString)));

        services.Configure<CatalogDbSettings>(configuration.GetSection(nameof(CatalogDbSettings)));

        MongoDbSerialization.AddSerializationRules();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }

    public static IServiceCollection AddApplicationHealhtChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
        .AddCheck(checkName, () => HealthCheckResult.Healthy())
        .AddMongoDb(configuration.GetConnectionString(catalogDbConnectionString));

        return services;
    }
}
