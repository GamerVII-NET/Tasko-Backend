using MongoDB.Driver;
using Tasko.Domains.Models.Structural.Interfaces;

namespace Tasko.Server.Repositories.Abstracts
{
    public class UserRepositoryBase
    {
        public UserRepositoryBase(IMongoDatabase databaseContext) => UserCollection = databaseContext.GetCollection<IUser>("Users");

        internal IMongoCollection<IUser> UserCollection { get; set; }
    }
}
