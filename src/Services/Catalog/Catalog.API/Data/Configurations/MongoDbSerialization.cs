namespace Catalog.API.Data.Configurations;

public static class MongoDbSerialization
{
    public static void AddSerializationRules()
    {
        ConventionRegistry.Register("CamelCase", new ConventionPack { new CamelCaseElementNameConvention() }, _ => true);
        ConventionRegistry.Register("IgnoreIfNull", new ConventionPack { new IgnoreIfNullConvention(true) }, _ => true);
        ConventionRegistry.Register("EnumToString", new ConventionPack { new EnumRepresentationConvention(BsonType.String) }, _ => true);
#pragma warning disable CS0618
        BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3; // obsolete: will be removed in later release of mongo driver
#pragma warning restore CS0618
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        BsonSerializer.RegisterSerializer(new DecimalSerializer(BsonType.Decimal128));
    }
}
