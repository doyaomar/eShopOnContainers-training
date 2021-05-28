using Catalog.API.Infrastructure;
using Catalog.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.Bootsrap
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
            .AddScoped<ICatalogRepository, CatalogRepository>()
            .AddScoped<ICatalogService, CatalogService>();

            return services;
        }
        
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
            .AddDbContext<CatalogContext>(opt => opt.UseSqlServer(configuration["ConnectionStrings:CatalogDb"]));

            return services;
        }

        public static IServiceCollection AddCustomHealhChecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
            .AddDbContextCheck<CatalogContext>();

            return services;
        }
    }
}