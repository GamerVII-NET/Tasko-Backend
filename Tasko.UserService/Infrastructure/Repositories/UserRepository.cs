using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Tasko.Service.Infrastructure.Repositories;

internal class UserRepository : BaseUserRepository, IUserRepository
{
    public UserRepository(IMongoDatabase mongoDatabase, ValidationParameter validationParameter) : base(mongoDatabase, validationParameter) { }

    public async Task<IUser> FindUserAsync(Guid userGuid)
    {
        var user = await UserCollection.Find(u => u.Id == userGuid).SingleOrDefaultAsync();
        return user;
    }
    public async Task<IEnumerable<IUser>> GetUsersAsync() => await UserCollection.Find(_ => true).ToListAsync();


}