using MongoDB.Driver;

namespace Tasko.Mongo.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class MongoDatabaseCollection<T> : Attribute
{
    public IMongoCollection<T> Collection { get; set; }
    public string CollectionName { get; set; }

	public MongoDatabaseCollection(string collectionName)
	{
        CollectionName = collectionName;
    }
}