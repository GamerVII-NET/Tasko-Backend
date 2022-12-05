﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Tasko.Domains.Models.DTO.Interfaces;
using Tasko.Domains.Models.DTO.Providers;
using Tasko.Domains.Models.Structural.Providers;
using Tasko.Server.Repositories.Interfaces;

namespace Tasko.Server.Services;

public class UserService
{
    internal static Func<IUserRepository, IMapper, Task<IResult>> GetUsers()
    {
        return [Authorize] async (IUserRepository repository, IMapper mapper) =>
        {
            var users = await repository.GetUsersAsync();
            return Results.Ok(mapper.Map<IEnumerable<IUserRead>>(users));
        };
    }

    internal static Func<IUserRepository, IMapper, UserCreate, Task<IResult>> CreateUser(string key, string issuer, string audience)
    {
        return [Authorize] async (IUserRepository repository, IMapper mapper, UserCreate userCreate) =>
        {
            var user = mapper.Map<User>(userCreate);
            var foundedUser = await repository.FindUserAsync(user.Email);
            if (foundedUser != null) return Results.Text("User already exsist!");
            await repository.CreateUserAsync(user);
            var token = repository.CreateToken(key, issuer, audience, user);
            return Results.Created($"/api/users/{user.Id}",
            new
            {
                User = mapper.Map<UserRead>(user),
                Token = token
            });
        };
    }

    internal static Func<IUserRepository, IMapper, Guid, Task<IResult>> FindUser()
    {
        return [Authorize] async (IUserRepository userRepository, IMapper mapper, Guid id) =>
        {
            var user = userRepository.FindUserAsync(id);
            return user == null ? Results.NotFound(id) : Results.Ok(user);
        };
    }

}
