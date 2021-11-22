using Catalog.API.Infrastructure;
using Catalog.API.Infrastructure.Serialization;
using Catalog.API.Infrastructure.Settings;
using Catalog.API.SeedWork;
using Catalog.API.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Catalog.API.Bootsrap;

public static class ServiceCollectionExtenions
{

    public static IServiceCollection AddUserServices(this IServiceCollection services)
    {
        services
        .AddSingleton<ICatalogDbContext, CatalogDbContext>()
        .AddScoped<ICatalogService, CatalogService>()
        .AddScoped<IGuidProvider, GuidProvider>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add configurations
        services
        .Configure<CatalogDbSettings>(configuration.GetSection(nameof(CatalogDbSettings)));
        // Add mongo mappings
        MongoDbSerialization.AddSerializationRules();

        return services;
    }

    public static IServiceCollection AddCustomHealhChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
        .AddCheck("self", () => HealthCheckResult.Healthy())
        .AddMongoDb(configuration.GetSection("CatalogDbSettings:ConnectionString").Value,
                    configuration.GetSection("CatalogDbSettings:DatabaseName").Value,
                    null,
                    null,
                    null,
                    null);

        return services;
    }
}
