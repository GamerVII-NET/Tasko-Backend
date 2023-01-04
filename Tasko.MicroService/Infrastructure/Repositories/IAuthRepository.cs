using System.Net;
using Tasko.Domains.Models.Structural;
using Tasko.Jwt.Models;

namespace Tasko.MicroService.Infrastructure.Repositories
{
    public interface IAuthRepository
    {
        Task<IResult> AuthorizationAsync(UserAuth userAuth, ValidationParameter jwtValidationParameter, IMapper mapper, IValidator<IUserAuth> validator, IPAddress ipAddress, IResponseCookies cookies);
        Task<IUser> FindUserAsync(string login);
        Task<IEnumerable<IRole>> GetUserRoles(User user);
        Task<IEnumerable<IPermission>> GetUserRolesPermissions(User user);
        Task<IEnumerable<IPermission>> GetUserPermissions(User user);
        Task SaveRefreshToken(User user, string refreshToken, string ipAddress);
        Task<IResult> RefreshTokenAuthorizationAsync(HttpContext context, IMapper mapper, ValidationParameter jwtValidationParameter);
    }
}