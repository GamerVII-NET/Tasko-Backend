using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Tasko.General.Models;

namespace Tasko.Service.Infrastructure.Repositories;

internal class UserRepository : BaseUserRepository, IUserRepository
{
    public UserRepository(IMongoDatabase mongoDatabase, JwtValidationParameter jwtValidationParameter) : base(mongoDatabase, jwtValidationParameter)
    {
    }

    public async Task<IUser> FindUserAsync(Guid userGuid)
    {
        var test = await UserCollection.Find(u => u.Id == userGuid).SingleOrDefaultAsync();

        return test;
    }
    public async Task<IEnumerable<IUser>> GetUsersAsync() => await UserCollection.Find(_ => true).ToListAsync();

    public Guid GetUserIdFromToken(string token)
    {
        if (string.IsNullOrEmpty(token))
            return Guid.Empty;

        var validationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = _jwtValidationParameter.SymmetricSecurityKey,
            ValidAudience = _jwtValidationParameter.Audienece,
            ValidIssuer = _jwtValidationParameter.Issuer,
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
    }

}