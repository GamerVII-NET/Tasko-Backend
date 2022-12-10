using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Tasko.AuthService.Infrastructure.Repositories;
using Tasko.Domains.Models.DTO.User;
using Tasko.General.Models;

namespace Tasko.AuthService.Infrastructure.Functions
{
    public class AuthFunctions
    {
        public static Func<IAuthRepository, IMapper, UserAuth, Task<IResult>> BearerAuthorization(JwtValidationParameter jwtValidationParmeter)
        {
            return [AllowAnonymous] async (IAuthRepository authRepository, IMapper mapper, UserAuth userAuth) =>
            Results.Ok(await authRepository.AuthorizationAsync(userAuth, jwtValidationParmeter));
        }
    }
}
