using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Tasko.Domains.Models.DTO.User;
using Tasko.Domains.Models.Structural.Providers;
using Tasko.Server.Repositories.Interfaces;

namespace Tasko.Server.Services;

public class UserService
{
    internal static Func<IUserRepository, IMapper, Guid, Task<IResult>> FindUser()
    {
        return [Authorize] async (IUserRepository userRepository, IMapper mapper, Guid id) =>
        {
            var user = userRepository.FindUserAsync(id);
            return user == null ? Results.NotFound(id) : Results.Ok(user);
        };
    }

    internal static Func<IUserRepository, IMapper, Task<IResult>> GetUsers()
    {
        return [Authorize] async (IUserRepository repository, IMapper mapper) =>
        {
            var users = await repository.GetUsersAsync();
            return Results.Ok(mapper.Map<IEnumerable<UserRead>>(users));
        };
    }

    internal static Func<IUserRepository, IMapper, UserCreate, Task<IResult>> CreateUser(string key, string issuer, string audience)
    {
        return /*[Authorize]*/ async (IUserRepository userRepository, IMapper mapper, UserCreate userCreate) =>
        {
            var user = mapper.Map<User>(userCreate);
            var foundedUser = await userRepository.FindUserAsync(user.Login);
            if (foundedUser != null) return Results.Conflict("User already exsist!");
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.IsDeleted = false;
            await userRepository.CreateUserAsync(user);
            var token = userRepository.CreateToken(key, issuer, audience, user);
            return Results.Created($"/api/users/{user.Id}",
            new
            {
                User = mapper.Map<UserRead>(user),
                Token = token
            });
        };
    }

    internal static Func<HttpContext, IUserRepository, IMapper, UserUpdate, Task<IResult>> UpdateUser(IConfiguration configuration)
    {
        return [Authorize] async (HttpContext context, IUserRepository userRepository, IMapper mapper, UserUpdate userUpdate) =>
        {
            var user = mapper.Map<User>(userUpdate);

            var foundedUser = await userRepository.FindUserAsync(user.Id);

            if (foundedUser == null) return Results.NotFound();

            var isCurrentUser = userRepository.VerifyUser(configuration, context, foundedUser);

            if (isCurrentUser == false) { return Results.Unauthorized(); }

            await userRepository.UpdateUserAsync(user);

            return Results.Ok();
        };
    }

    internal static Func<HttpContext, IUserRepository, IMapper, Guid, Task<IResult>> DeleteUser(IConfiguration configuration)
    {
        return [Authorize] async (HttpContext context, IUserRepository userRepository, IMapper mapper, Guid id) =>
        {
            var user = await userRepository.FindUserAsync(id);

            if (user is null)
            {
                Results.NotFound(id);
            }

            var isCurrentUser = userRepository.VerifyUser(configuration, context, user);

            if (isCurrentUser == false) { return Results.Unauthorized(); }

            await userRepository.DeleteUserAsync(user.Id);


            return Results.Ok();
        };
    }
}