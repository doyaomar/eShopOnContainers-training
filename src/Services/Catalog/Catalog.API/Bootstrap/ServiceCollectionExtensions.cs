using Catalog.API.Infrastructure;
using Catalog.API.Infrastructure.Settings;
using Catalog.API.SeedWork;
using Catalog.API.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

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
        AddMongoDbMappingRules();

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

    public static void AddMongoDbMappingRules()
    {
        ConventionRegistry.Register("CamelCase", new ConventionPack { new CamelCaseElementNameConvention() }, _ => true);
        ConventionRegistry.Register("IgnoreIfNull", new ConventionPack { new IgnoreIfNullConvention(true) }, _ => true);
        ConventionRegistry.Register("EnumToString", new ConventionPack { new EnumRepresentationConvention(BsonType.String) }, _ => true);

        BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(typeof(int), new Int32Serializer(BsonType.Double));
        BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Double));
    }
}
