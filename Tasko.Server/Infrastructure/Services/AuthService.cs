using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Tasko.Domains.Models.DTO.Providers;
using Tasko.Server.Repositories.Interfaces;

namespace Tasko.Server.Infrastructure.Services
{
    public class AuthService
    {
        public static Func<IUserRepository, IMapper, UserAuth, Task<IResult>> BearerAuthorization(string key, string issuer, string audience)
        {
            return [AllowAnonymous] async (IUserRepository userRepository, IMapper mapper, UserAuth userAuth) =>
            {
                var user = await userRepository.FindUserAsync(userAuth.Login);
                if (user == null) return Results.NotFound();
                var enhancedHashPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(userAuth.Password);
                var validatePassword = BCrypt.Net.BCrypt.EnhancedVerify(user.Password, enhancedHashPassword);
                if (!validatePassword) return Results.Unauthorized();
                string token = userRepository.CreateToken(key, issuer, audience, user);
                return Results.Ok(token);
            };
        }


    }
}
