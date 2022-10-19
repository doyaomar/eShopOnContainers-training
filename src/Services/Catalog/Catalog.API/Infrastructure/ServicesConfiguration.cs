namespace Catalog.API.Infrastructure;

public static class ServicesConfiguration
{
    private const string CatalogDbConnectionString = "CatalogDb";
    private const string SelfName = "self";

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services
        .AddScoped<IGuidService, GuidService>()
        .AddScoped<IFileService, FileService>()
        .AddScoped<IContentTypeProvider, FileExtensionContentTypeProvider>();

        var executingAssembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(executingAssembly);

        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

        services.AddMediatR(executingAssembly);

        return services;
    }

    public static IServiceCollection AddApplicationConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services
        .Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true)
        .Configure<CatalogSettings>(configuration)
        .Configure<CatalogDbSettings>(configuration.GetSection(nameof(CatalogDbSettings)));

        return services;
    }

    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        MongoDbBsonSerialization.RegisterConventionRegistry();
        MongoDbBsonSerialization.RegisterSerialization();
        services
        .AddScoped<ICatalogDbContext, CatalogDbContext>()
        .AddSingleton<IMongoClient>(_ => new MongoClient(configuration.GetConnectionString(CatalogDbConnectionString)));

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
