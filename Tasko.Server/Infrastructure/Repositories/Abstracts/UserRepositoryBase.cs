using MongoDB.Driver;
using Tasko.Domains.Models.Structural.Interfaces;
using Tasko.Domains.Models.Structural.Providers;

namespace Tasko.Server.Repositories.Abstracts
{
    public class UserRepositoryBase
    {
        public UserRepositoryBase(IMongoDatabase databaseContext) => UserCollection = databaseContext.GetCollection<User>("Users");

        internal IMongoCollection<User> UserCollection { get; set; }
    }
}
