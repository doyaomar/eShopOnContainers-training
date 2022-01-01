namespace Catalog.API.Data.Serialization;

public class MongoDbBsonSerializationProvider : IBsonSerializationProvider
{
    public IBsonSerializer GetSerializer(Type type)
    {
        if (type == typeof(Guid))
        {
            return new GuidSerializer(GuidRepresentation.Standard);
        }

        if (type == typeof(decimal))
        {
            return new DecimalSerializer(BsonType.Decimal128);
        }

        return null!;
    }
}