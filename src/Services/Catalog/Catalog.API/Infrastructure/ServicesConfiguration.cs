namespace Catalog.API.Infrastructure;

public static class ServicesConfiguration
{
    private const string CatalogDbConnectionString = "CatalogDb";
    private const string SelfName = "self";

    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();

        MongoDbBsonSerialization.RegisterConventionRegistry();
        MongoDbBsonSerialization.RegisterSerialization();

        services
        .AddScoped<IGuidService, GuidService>()
        .AddScoped<ICatalogDbContext, CatalogDbContext>()
        .AddScoped<IContentTypeProvider, FileExtensionContentTypeProvider>()
        .AddSingleton<IMongoClient>(_ => new MongoClient(configuration.GetConnectionString(CatalogDbConnectionString)));

        services.Configure<CatalogSettings>(configuration);
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
        .AddCheck(SelfName, () => HealthCheckResult.Healthy())
        .AddMongoDb(configuration.GetConnectionString(CatalogDbConnectionString));

        return services;
    }
}
