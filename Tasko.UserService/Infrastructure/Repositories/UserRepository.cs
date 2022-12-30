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

    public async Task<Guid> GetUserIdFromToken(string token)
    {
        if (string.IsNullOrEmpty(token)) return Guid.Empty;

        return await Task.Run(() =>
        {
            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = ValidationParameter.SymmetricSecurityKey,
                ValidAudience = ValidationParameter.Audienece,
                ValidIssuer = ValidationParameter.Issuer,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                if (validatedToken != null)
                {
                    var securityToken = (JwtSecurityToken)validatedToken;
                    if (securityToken == null) return Guid.Empty;
                    var userId = Guid.Parse(securityToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

                    return userId;
                }
            }
            catch (SecurityTokenException)
            {
                return Guid.Empty;
            }
            catch (Exception e)
            {
                return Guid.Empty;
            }
            return Guid.Empty;
        });   
    }

}