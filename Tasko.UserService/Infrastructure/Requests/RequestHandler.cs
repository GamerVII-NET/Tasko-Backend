using AutoMapper;
using FluentValidation.Results;
using System.Threading;
using Tasko.Domains.Models.DTO.User;
using Tasko.Domains.Models.RequestResponses;
using Tasko.Jwt.Extensions;
using Tasko.Jwt.Services;
using Tasko.Service.Infrastructure.Repositories;

namespace Tasko.Service.Infrastructure.Requests;

internal static class RequestHandler
{
    internal static Func<IUserRepository, Guid, Task<IResult>> FindUser()
    {
        return [Authorize] async (IUserRepository userRepository, Guid id) =>
        {
            var user = await userRepository.FindOneAsync(id);
            return user == null ? Results.NotFound(id) : Results.Ok(new RequestResponse<IUser>(user, StatusCodes.Status200OK));
        };
    }

    internal static Func<IUserRepository, ValidationParameter, IMapper, CancellationToken, Task<IResult>> GetUsers()
    {
        return async (IUserRepository userRepository, ValidationParameter jwtValidationParameter, IMapper mapper, CancellationToken cancellationToken) =>
        {
            var users = mapper.Map<IEnumerable<IUserRead>>(await userRepository.GetAsync(cancellationToken));
            
            return Results.Ok(new GetRequestResponse<IUserRead>(users));
        };
    }

    internal static Func<IUserRepository, IMapper, UserCreate, IValidator<IUserCreate>, CancellationToken, Task<IResult>> CreateUser(ValidationParameter validationParameter)
    {
        return async (IUserRepository userRepository, IMapper mapper, UserCreate userCreate, IValidator<IUserCreate> validator, CancellationToken cancellationToken) =>
        {
            var validationResult = validator.Validate(userCreate);
            
            if (!validationResult.IsValid)
            {
                var result = new BadRequestResponse<List<ValidationFailure>>(validationResult.Errors, "Validation error");
                return Results.BadRequest(result);
            }

            var user = mapper.Map<User>(userCreate);
            var foundedUser = await userRepository.FindOneAsync(u => u.Login.Equals(user.Login), cancellationToken);

            if (foundedUser != null)
                return Results.Conflict(new BadRequestResponse<UserCreate>(userCreate, "User already exsist", StatusCodes.Status409Conflict));

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.IsDeleted = false;
            await userRepository.CreateAsync(user);
            var userPermissions = await userRepository.GetUserPermissions(user);
            var userRolesPermissions = await userRepository.GetUserRolesPermissions(user);
            var permissions = userPermissions.Concat(userRolesPermissions).ToList();
            var token = JwtServices.CreateToken(validationParameter, user, userPermissions);

            var response = new UserAuthRead
            {
                User = mapper.Map<UserRead>(user),
                Token = token
            };

            return Results.Created($"/api/users/{user.Id}", new RequestResponse<IUserAuthRead>(response, StatusCodes.Status200OK));
        };
    }

    internal static Func<HttpContext, IUserRepository, IMapper, UserUpdate, IValidator<IUserUpdate>, Task<IResult>> UpdateUser(ValidationParameter validationParmeter)
    {
        return [Authorize] async (HttpContext context, IUserRepository userRepository, IMapper mapper, UserUpdate userUpdate, IValidator<IUserUpdate> validator) =>
        {
            var validationResult = validator.Validate(userUpdate);

            if (!validationResult.IsValid)
            {
                var result = new BadRequestResponse<List<ValidationFailure>>(validationResult.Errors, "Validation error");
                return Results.BadRequest(result);
            }

            var user = mapper.Map<User>(userUpdate);

            var foundedUser = await userRepository.FindOneAsync(user.Id);

            if (foundedUser == null)
                return Results.Conflict(new BadRequestResponse<UserUpdate>(userUpdate, "User already exsist", StatusCodes.Status409Conflict));


            var token = context.GetJwtToken();
            var isCurrentUser = JwtServices.VerifyUser(token, validationParmeter, foundedUser);

            if (isCurrentUser == false) { return Results.Unauthorized(); }

            foundedUser.Email = user.Email;
            foundedUser.FirstName = user.FirstName;
            foundedUser.LastName = user.LastName;
            foundedUser.Patronymic = user.Patronymic;
            foundedUser.About = user.About;
            foundedUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await userRepository.UpdateAsync(mapper.Map<User>(foundedUser));

            var response = mapper.Map<User, UserRead>(user);

            return Results.Ok(new RequestResponse<IUserRead>(response, StatusCodes.Status200OK));
        };
    }
    internal static Func<HttpContext, IUserRepository, IMapper, Guid, Task<IResult>> DeleteUser(ValidationParameter validationParmeter)
    {
        return [Authorize] async (HttpContext context, IUserRepository userRepository, IMapper mapper, Guid id) =>
        {
            var user = await userRepository.FindOneAsync(id);

            if (user is null)
            {
                var result = new List<ValidationFailure>
                {
                    new ValidationFailure("User", $"User with unique identificator {id} not found", "User not found")
                };

                return Results.NotFound(new BadRequestResponse<List<ValidationFailure>>(result, "User not found", StatusCodes.Status404NotFound));
            }
            var token = context.GetJwtToken();
            var isCurrentUser = JwtServices.VerifyUser(token, validationParmeter, user);

            if (isCurrentUser == false) return Results.Unauthorized();

            var deletedUser = await userRepository.DeleteAsync(user.Id);
            return Results.Ok(deletedUser);
        };
    }
    internal static Func<HttpContext, IUserRepository, IMapper, Guid, Task<IResult>> GetRefreshTokens(ValidationParameter validationParmeter)
    {
        return [Authorize] async (HttpContext context, IUserRepository userRepository, IMapper mapper, Guid id) =>
        {
            var user = await userRepository.FindOneAsync(id);

            if (user is null)
            {
                var result = new List<ValidationFailure>
                {
                    new ValidationFailure("User", $"User with unique identificator {id} not found", "User not found")
                };

                return Results.NotFound(new BadRequestResponse<List<ValidationFailure>>(result, "User not found", StatusCodes.Status404NotFound));
            }
            var token = context.GetJwtToken();
            var isCurrentUser = JwtServices.VerifyUser(token, validationParmeter, user);

            if (isCurrentUser == false) return Results.Unauthorized();

            //var refreshTokens = await userRepository.GetRefreshTokensAsync(user.Id);

            //var refreshTokenResult = new GetRequestResponse<IRefreshToken>(refreshTokens);

            //return Results.Ok(refreshTokenResult);

            return Results.Ok();
        };
    }
}