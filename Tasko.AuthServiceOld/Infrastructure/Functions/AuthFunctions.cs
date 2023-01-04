using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Tasko.AuthService.Infrastructure.Repositories;
using Tasko.Domains.Models.DTO.User;

namespace Tasko.AuthService.Infrastructure.Functions
{
    public class AuthFunctions
    {
        public static Func<HttpContext, IAuthRepository, UserAuth, IMapper, IValidator<IUserAuth>, Task<IResult>> BearerAuthorization(JwtValidationParameter jwtValidationParmeter)
        {
            return async (HttpContext context, IAuthRepository authRepository, UserAuth userAuth, IMapper mapper, IValidator<IUserAuth> validator) =>
                await authRepository.AuthorizationAsync(userAuth, 
                jwtValidationParmeter, 
                mapper, validator, 
                context.Connection.RemoteIpAddress, 
                context.Response.Cookies);
        }
        public static Func<HttpContext, IAuthRepository, IMapper, Task<IResult>> RefreshTokenAuthorization(JwtValidationParameter jwtValidationParameter)
        {
            return async (HttpContext context, IAuthRepository authRepository, IMapper mapper) =>
                await authRepository.RefreshTokenAuthorizationAsync(context, mapper, jwtValidationParameter);
        }



    }
}
