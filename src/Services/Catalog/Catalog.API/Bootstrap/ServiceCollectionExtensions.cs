using Catalog.API.Infrastructure;
using Catalog.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Catalog.API.Bootsrap;

public static class ServiceCollectionExtenions
{

    public static IServiceCollection AddUserServices(this IServiceCollection services)
    {
        services
        .AddScoped<ICatalogRepository, CatalogRepository>()
        .AddScoped<ICatalogService, CatalogService>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
        .AddDbContext<CatalogContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("CatalogDb")));

        return services;
    }

    public static IServiceCollection AddCustomHealhChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
        .AddCheck("self", () => HealthCheckResult.Healthy())
        .AddDbContextCheck<CatalogContext>();

        return services;
    }
}
