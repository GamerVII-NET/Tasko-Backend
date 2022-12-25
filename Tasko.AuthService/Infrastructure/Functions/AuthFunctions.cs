using System.Net;
using Tasko.AuthService.Infrastructure.Repositories;

namespace Tasko.AuthService.Infrastructure.Functions
{
    public class AuthFunctions
    {
        public static Func<HttpContext, IAuthRepository, UserAuth, IMapper, IValidator<IUserAuth>, Task<IResult>> BearerAuthorization(JwtValidationParameter jwtValidationParmeter)
        {
            return async (HttpContext context, IAuthRepository authRepository, UserAuth userAuth, IMapper mapper, IValidator<IUserAuth> validator) =>
                   await authRepository.AuthorizationAsync(userAuth, jwtValidationParmeter, mapper, validator, context.Connection.RemoteIpAddress);
        }

    }
}
