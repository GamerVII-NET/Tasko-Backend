using Tasko.AuthService.Infrastructure.Repositories;
using Tasko.Jwt.Models;

namespace Tasko.AuthService.Infrastructure.RequestHandlers
{
    public class AuthRequestHandler
    {
        public static Func<HttpContext, IAuthRepository, UserAuth, IMapper, IValidator<IUserAuth>, CancellationToken, Task<IResult>> BearerAuthorization(ValidationParameter jwtValidationParmeter)
        {
            return async (context, authRepository, userAuth, mapper, validator, cancellationToken) =>
                   await authRepository.AuthorizationAsync(userAuth,
                                                           jwtValidationParmeter,
                                                           mapper, 
                                                           validator,
                                                           context.Connection.RemoteIpAddress,
                                                           context.Response.Cookies,
                                                           cancellationToken);
        }
        public static Func<HttpContext, IAuthRepository, IMapper, CancellationToken, Task<IResult>> RefreshTokenAuthorization(ValidationParameter jwtValidationParameter)
        {
            return async (context, authRepository, mapper, cancellationToken) =>
                   await authRepository.RefreshTokenAuthorizationAsync(context, mapper, jwtValidationParameter, cancellationToken);
        }
    }
}
