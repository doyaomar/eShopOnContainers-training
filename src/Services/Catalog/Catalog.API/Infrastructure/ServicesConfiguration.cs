namespace Catalog.API.Infrastructure;

public static class ServicesConfiguration
{
    private const string _catalogDbConnectionString = "CatalogDb";
    private const string _selfName = "self";

    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();

        MongoDbBsonSerialization.RegisterConventionRegistry();
        MongoDbBsonSerialization.RegisterSerialization();

        services
        .AddScoped<IGuidService, GuidService>()
        .AddScoped<ICatalogDbContext, CatalogDbContext>()
        .AddSingleton<IMongoClient>(_ => new MongoClient(configuration.GetConnectionString(_catalogDbConnectionString)));

        services.Configure<CatalogDbSettings>(configuration.GetSection(nameof(CatalogDbSettings)));

        services.AddAutoMapper(executingAssembly);

        services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(executingAssembly));

        services.AddMediatR(executingAssembly);

        return services;
    }

    public static IServiceCollection AddApplicationHealhtChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
        .AddHealthChecks()
        .AddCheck(_selfName, () => HealthCheckResult.Healthy())
        .AddMongoDb(configuration.GetConnectionString(_catalogDbConnectionString));

        return services;
    }
}
