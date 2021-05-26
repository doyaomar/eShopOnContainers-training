using Catalog.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.Bootsrap
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Inject db contexts
            string catalogDb = "ConnectionStrings:CatalogDb";
            services.AddDbContext<CatalogContext>(opt => opt.UseSqlServer(configuration[catalogDb]));

            return services;
        }
    }
}