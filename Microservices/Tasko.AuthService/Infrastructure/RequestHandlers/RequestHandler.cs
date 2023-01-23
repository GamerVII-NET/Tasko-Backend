using Tasko.AuthService.Infrastructure.Repositories;
using Tasko.Jwt.Models;

namespace Tasko.AuthService.Infrastructure.RequestHandlers
{
    public class RequestHandler
    {
        public static Func<HttpContext, IAuthRepository, UserAuth, IMapper, IValidator<IUserAuth>, Task<IResult>> BearerAuthorization(ValidationParameter jwtValidationParmeter)
        {
            return async (context, authRepository, userAuth, mapper, validator) =>
                   await authRepository.AuthorizationAsync(userAuth,
                                                           jwtValidationParmeter,
                                                           mapper, 
                                                           validator,
                                                           context.Connection.RemoteIpAddress,
                                                           context.Response.Cookies);
        }
        public static Func<HttpContext, IAuthRepository, IMapper, Task<IResult>> RefreshTokenAuthorization(ValidationParameter jwtValidationParameter)
        {
            return async (context, authRepository, mapper) =>
                   await authRepository.RefreshTokenAuthorizationAsync(context, mapper, jwtValidationParameter);
        }
    }
}
