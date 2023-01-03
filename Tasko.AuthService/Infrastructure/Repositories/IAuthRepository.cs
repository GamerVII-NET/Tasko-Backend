using System.Net;
using Tasko.Domains.Models.Structural;
using Tasko.Jwt.Models;

namespace Tasko.AuthService.Infrastructure.Repositories;
public interface IAuthRepository
{
    Task<IResult> AuthorizationAsync(IUserAuth userAuth, ValidationParameter jwtValidationParameter, IMapper mapper, IValidator<IUserAuth> validator, IPAddress ipAddress, IResponseCookies cookies);
    Task<IUser> FindUserAsync(string login);
    Task<List<Role>> GetUserRoles(IUser user);
    Task<List<Permission>> GetUserRolesPermissions(IUser user);
    Task<List<Permission>> GetUserPermissions(IUser user);
    Task SaveRefreshToken(IUser user, string refreshToken, string ipAddress);
    Task<IResult> RefreshTokenAuthorizationAsync(HttpContext context, IMapper mapper, ValidationParameter jwtValidationParameter);
}