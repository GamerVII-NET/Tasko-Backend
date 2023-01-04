using Tasko.Jwt.Models;
using Tasko.MicroService.Infrastructure.Repositories;

namespace Tasko.MicroService.Infrastructure.RequestHandlers
{
    public class AuthRequestHandler
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
