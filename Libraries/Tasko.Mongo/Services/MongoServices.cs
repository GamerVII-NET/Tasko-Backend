using MongoDB.Driver;

namespace Tasko.Mongo.Services;
public static class MongoServices
{
    public static IMongoDatabase GetMongoDataConext(string connection, string databaseName)
    {
        var mongoClient = new MongoClient(connection);
        var databaseContext = mongoClient.GetDatabase(databaseName);
        return databaseContext;
    }
}