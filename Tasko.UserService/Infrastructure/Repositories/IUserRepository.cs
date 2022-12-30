using Tasko.General.Extensions.Jwt;
using Tasko.General.Models;

namespace Tasko.Service.Infrastructure.Repositories;

internal interface IUserRepository
{
    JwtValidationParameter _jwtValidationParameter { get; set; }
    IMongoCollection<IUser> UserCollection { get; set; }
    FilterDefinitionBuilder<User> UserFilter { get; }
    Task<IUser> FindUserAsync(Guid userGuid);
    Guid GetUserIdFromToken(string token);
    Task<IEnumerable<IUser>> GetUsersAsync();
}

internal class BaseUserRepository
{
    public JwtValidationParameter _jwtValidationParameter { get; set; }

    public IMongoCollection<IUser> UserCollection { get; set; }

    public BaseUserRepository(IMongoDatabase mongoDatabase, JwtValidationParameter jwtValidationParameter)
    {
        UserCollection = mongoDatabase.GetCollection<IUser>("Users");

        _jwtValidationParameter = jwtValidationParameter;
    }
}