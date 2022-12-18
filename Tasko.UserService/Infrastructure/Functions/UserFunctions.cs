using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Tasko.Domains.Models.DTO.User;
using Tasko.Domains.Models.Structural.Providers;
using Tasko.General.Commands;
using Tasko.General.Models.RequestResponses;
using Tasko.UserService.Infrasructure.Repositories;

namespace Tasko.UserService.Infrasructure.Functions;

public class UserFunctions
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

            return Results.Ok(new GetRequestResponse<UserRead>(mapper.Map<IEnumerable<UserRead>>(users)));
        };
    }

    internal static Func<IUserRepository, IMapper, UserCreate, IValidator<IUserCreate>, Task<IResult>> CreateUser(JwtValidationParameter jwtValidationParameter)
    {
        return async (IUserRepository userRepository, IMapper mapper, UserCreate userCreate, IValidator<IUserCreate> validator) =>
        {
            var validationResult = validator.Validate(userCreate);

            if (!validationResult.IsValid)
            {
                var result = new BadRequestResponse<List<ValidationFailure>>(validationResult.Errors, "Ошибка при валидации данных");
                return Results.BadRequest(result);
            }

            var user = mapper.Map<User>(userCreate);
            var foundedUser = await userRepository.FindUserAsync(user.Login);

            if (foundedUser != null) return Results.Conflict(new BadRequestResponse<string>(
                "Пользователь с указанными данными уже существует", 
                "Пользователь с указанными данными уже существует", 
                StatusCodes.Status409Conflict));

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.IsDeleted = false;
            await userRepository.CreateUserAsync(user);
            var token = Jwt.CreateToken(jwtValidationParameter, user);

            var response = new UserAuthRead
            {
                User = mapper.Map<UserRead>(user),
                Token = token
            };
          
            return Results.Created($"/api/users/{user.Id}", new RequestResponse<IUserAuthRead>(response, StatusCodes.Status200OK));
        };
    }

    internal static Func<HttpContext, IUserRepository, IMapper, UserUpdate, IValidator<IUserUpdate>, Task<IResult>> UpdateUser(JwtValidationParameter jwtValidationParmeter)
    {
        return [Authorize] async (HttpContext context, IUserRepository userRepository, IMapper mapper, UserUpdate userUpdate, IValidator<IUserUpdate> validator) =>
        {
            var validationResult = validator.Validate(userUpdate);

            if (!validationResult.IsValid)
            {
                var result = new BadRequestResponse<List<ValidationFailure>>(validationResult.Errors, "Ошибка при валидации данных");
                return Results.BadRequest(result);
            }

            var user = mapper.Map<User>(userUpdate);

            var foundedUser = await userRepository.FindUserAsync(user.Id);

            if (foundedUser == null)
                return Results.Conflict(new BadRequestResponse<string>(
                "Пользователь с указанными данными не сущуствует!",
                "Пользователь с указанными данными не сущуствует!",
                StatusCodes.Status404NotFound));

            var isCurrentUser = Jwt.VerifyUser(context, jwtValidationParmeter, foundedUser);

            if (isCurrentUser == false) { return Results.Unauthorized(); }

            foundedUser.Email = user.Email;
            foundedUser.FirstName = user.FirstName;
            foundedUser.LastName = user.LastName;
            foundedUser.Patronymic = user.Patronymic;
            foundedUser.About = user.About;
            foundedUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await userRepository.UpdateUserAsync(mapper.Map<User>(foundedUser));

            var response = mapper.Map<User, UserRead>(user);

            return Results.Ok(new RequestResponse<IUserRead>(response, StatusCodes.Status200OK));
        };
    }

    internal static Func<HttpContext, IUserRepository, IMapper, Guid, Task<IResult>> DeleteUser(JwtValidationParameter jwtValidationParmeter)
    {
        return [Authorize] async (HttpContext context, IUserRepository userRepository, IMapper mapper, Guid id) =>
        {
            var user = await userRepository.FindUserAsync(id);

            if (user is null)
            {
                return Results.NotFound(new BadRequestResponse<string>($"Пользователь с указанным идентификатором {id} не найден", "Пользователь не найден.", StatusCodes.Status404NotFound));
            }

            var isCurrentUser = Jwt.VerifyUser(context, jwtValidationParmeter, user);

            if (isCurrentUser == false) { return Results.Unauthorized(); }

            await userRepository.DeleteUserAsync(user.Id);


            return Results.Ok();
        };
    }
}