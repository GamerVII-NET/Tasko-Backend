using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Tasko.Domains.DTOs.User;
using Tasko.Server.Context.Data.Models;
using Tasko.Server.Repositories.UserRepository;

namespace Tasko.Server.Services.API;

public class UserService
{
    internal static Func<IUserRepository, IMapper, Task<IResult>> GetUsersList()
    {
        return async (IUserRepository repository, IMapper mapper) =>
        {
            var users = await repository.GetAllUsersAsync();

            return Results.Ok(mapper.Map<IEnumerable<UserReadDto>>(users));
        };
    }

    internal static Func<IUserRepository, IMapper, UserCreateDto, Task<IResult>> CreateUser(WebApplicationBuilder builder)
    {
        return async (IUserRepository repository, IMapper mapper, UserCreateDto user) =>
        {
            User userModel = mapper.Map<User>(user);

            //var checkUser = await repository.GetUserByUserNameAsync(user.UserName);

            //if (checkUser != null)
            //{
            //    return Results.Conflict(new { Message = "An account with such data has already been registered" });
            //}

            userModel = await repository.CreateUserAsync(userModel);

            await repository.SaveChangesAsync();

            var token = repository.CreateToken(
                    builder.Configuration["Jwt:Key"],
                    builder.Configuration["Jwt:Issuer"],
                    userModel
                    );

            return Results.Created($"/api/v1/users/{userModel.GlobalGuid}", new
            {
                User = mapper.Map<UserReadDto>(userModel),
                Token = token
            });
        };
    }
}
