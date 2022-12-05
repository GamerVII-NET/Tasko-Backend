using AutoMapper;
using Tasko.Domains.Models.DTO.Interfaces;
using Tasko.Server.Repositories.Interfaces;

namespace Tasko.Server.Infrastructure.Services
{
    public class AuthService
    {
        public static Func<IUserRepository, IMapper, IUserAuth, Task<IResult>> BearerAuthorization(string key, string issuer, string audience)
        {
            return async (IUserRepository userRepository, IMapper mapper, IUserAuth userAuth) =>
            {
                var user = await userRepository.FindUserAsync(userAuth.Login, userAuth.Password);
                if (user == null) return Results.Unauthorized();
                string token = userRepository.CreateToken(key, issuer, audience, user);
                return Results.Ok(token);
            };
        }


    }
}
