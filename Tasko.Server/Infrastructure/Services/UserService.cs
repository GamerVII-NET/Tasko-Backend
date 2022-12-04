using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tasko.Domains.Models.DTO.Interfaces;
using Tasko.Domains.Models.Structural.Providers;
using Tasko.Server.Repositories.Interfaces;

namespace Tasko.Server.Services;

public class UserService
{
    internal static Func<IUserRepository, IMapper, Task<IResult>> GetUsers()
    {
        return async (IUserRepository repository, IMapper mapper) =>
        {
            var users = await repository.GetUsersAsync();
            return Results.Ok(mapper.Map<IEnumerable<IUserRead>>(users));
        };
    }

    internal static Func<IUserRepository, IMapper, IUserCreate, Task<IResult>> CreateUser(string key, string issuer)
    {
        return async (IUserRepository repository, IMapper mapper, IUserCreate userCreate) =>
        {
            var user = mapper.Map<User>(userCreate);
            await repository.CreateUserAsync(user);
            var token = repository.CreateToken(key, issuer, user);
            return Results.Created($"/api/v1/users/{user.Id}",
            new
            {
                User = mapper.Map<IUserRead>(user),
                Token = token
            });
        };
    }
}
