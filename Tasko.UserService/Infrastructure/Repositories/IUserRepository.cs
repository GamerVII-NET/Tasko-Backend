using Tasko.Domains.Models.DTO.User;

namespace Tasko.Service.Infrastructure.Repositories;

internal interface IUserRepository : IRepository<IUser>
{

}

internal abstract class BaseUserRepository
{
    internal ValidationParameter ValidationParameter { get; init; }
    internal FilterDefinitionBuilder<User> UserFilter { get; init; }
    internal IMongoCollection<IUser> UserCollection { get; init; }
    internal BaseUserRepository(IMongoDatabase mongoDatabase, ValidationParameter validationParameter)
    {
        ValidationParameter = validationParameter;
        UserCollection = mongoDatabase.GetCollection<IUser>("Users");
        UserFilter = Builders<User>.Filter;
    }
}