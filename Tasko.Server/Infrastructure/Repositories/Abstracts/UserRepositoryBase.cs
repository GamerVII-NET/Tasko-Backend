using MongoDB.Driver;
using Tasko.Domains.Models.Structural.Providers;

namespace Tasko.Server.Repositories.Abstracts
{
    public class UserRepositoryBase
    {
        public UserRepositoryBase(IMongoDatabase databaseContext)
        {
            Filter = Builders<User>.Filter;
            UserCollection = databaseContext.GetCollection<User>("Users");
        }

        internal IMongoCollection<User> UserCollection { get; set; }

        internal FilterDefinitionBuilder<User> Filter { get; }
    }
}
