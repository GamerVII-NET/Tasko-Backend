using MongoDB.Driver;

namespace Tasko.General.Commands
{
    public static class Mongo
    {
        public static IMongoDatabase GetMongoDataConext(string connection, string databaseName)
        {
            var mongoClient = new MongoClient(connection);
            var databaseContext = mongoClient.GetDatabase(databaseName);
            return databaseContext;
        }
    }
}
