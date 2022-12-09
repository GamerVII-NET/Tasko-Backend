using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using Tasko.Domains.Models.DTO.User;
using Tasko.Domains.Models.Structural.Providers;
using Tasko.Server.Repositories.Interfaces;

namespace Tasko.Server.Services;

public class RoleService
{

    internal static Func<HttpContent, IUserRepository, IRoleRepository, IMapper, UserRoleCreate, Task<IResult>> CreateRole (string key, string issuer, string audience)
    {
        return [Authorize] async (HttpContent httpContext, IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper, UserRoleCreate userRoleCreate) =>
        {
            var foundedUser = await userRepository.FindUserAsync(userRoleCreate.UserId);

            if (foundedUser == null) return Results.Conflict("User not found!");

        };
    }
    //internal static Func<HttpContent, IUserRepository, IRoleRepository, IMapper, UserRoleCreate, Task<IResult>> CreateRole(string key, string issuer, string audience)
    //{
    //    return /*[Authorize]*/ async (HttpContent httpContext, IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper, UserRoleCreate userRoleCreate) =>
    //    {
    //        var foundedUser = await userRepository.FindUserAsync(userRoleCreate.UserId);

    //        if (foundedUser == null) return Results.Conflict("User not found!");


    //        var token = userRepository.CreateToken(key, issuer, audience, user);
    //        return Results.Created($"/api/users/{user.Id}",
    //        new
    //        {
    //            User = mapper.Map<UserRead>(user),
    //            Token = token
    //        });
    //    };
    //}
}