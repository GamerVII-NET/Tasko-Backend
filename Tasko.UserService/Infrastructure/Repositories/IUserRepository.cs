namespace Tasko.Service.Infrastructure.Repositories;

internal interface IUserRepository
{
    Task<IUser> FindUserAsync(Guid userGuid);
    Task<IEnumerable<IUser>> GetUsersAsync();
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