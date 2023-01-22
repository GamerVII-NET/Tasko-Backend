using System.Net;
using Tasko.Domains.Models.Structural;
using Tasko.Jwt.Models;

namespace Tasko.AuthService.Infrastructure.Repositories;
public interface IAuthRepository
{
    Task<IResult> AuthorizationAsync(UserAuth userAuth, ValidationParameter jwtValidationParameter, IMapper mapper, IValidator<IUserAuth> validator, IPAddress ipAddress, IResponseCookies cookies, CancellationToken cancellationToken);
    Task<IUser> FindUserAsync(string login);
    Task<IEnumerable<IRole>> GetUserRoles(User user, CancellationToken cancellationToken);
    Task<IEnumerable<IPermission>> GetUserRolesPermissions(User user, CancellationToken cancellationToken);
    Task<IEnumerable<IPermission>> GetUserPermissions(User user, CancellationToken cancellationToken);
    Task SaveRefreshToken(User user, string refreshToken, string ipAddress, CancellationToken cancellationToken);
    Task<IResult> RefreshTokenAuthorizationAsync(HttpContext context, IMapper mapper, ValidationParameter jwtValidationParameter, CancellationToken cancellationToken);
}