using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

namespace Catalog.API.Infrastructure.Serialization;

public static class MongoDbSerialization
{
    public static void AddSerializationRules()
    {
        ConventionRegistry.Register("CamelCase", new ConventionPack { new CamelCaseElementNameConvention() }, _ => true);
        ConventionRegistry.Register("IgnoreIfNull", new ConventionPack { new IgnoreIfNullConvention(true) }, _ => true);
        ConventionRegistry.Register("EnumToString", new ConventionPack { new EnumRepresentationConvention(BsonType.String) }, _ => true);

        BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3; // obsolete: will be removed in later release of mongo driver
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        BsonSerializer.RegisterSerializer(new Int32Serializer(BsonType.Double));
        BsonSerializer.RegisterSerializer(new DecimalSerializer(BsonType.Double));
    }
}