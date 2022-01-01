namespace Catalog.API.Data.Serialization;

public static class MongoDbBsonSerialization
{
    public static void RegisterConventionRegistry()
    {
        ConventionRegistry.Register(
            "CamelCase",
            new ConventionPack { new CamelCaseElementNameConvention() },
            _ => true);
        ConventionRegistry.Register(
            "IgnoreIfNull",
            new ConventionPack { new IgnoreIfNullConvention(true) },
            _ => true);
        ConventionRegistry.Register(
            "EnumToString",
            new ConventionPack { new EnumRepresentationConvention(BsonType.String) },
            _ => true);
    }

    public static void RegisterSerialization()
    {
#pragma warning disable CS0618
        BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3; // obsolete: will be removed in later release of mongo driver
#pragma warning restore CS0618
        BsonSerializer.RegisterSerializationProvider(new MongoDbBsonSerializationProvider());

    }
}
