using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Tasko.Domains.Models.DTO.User;
using Tasko.General.Models;
using Tasko.General.Models.RequestResponses;
using Tasko.UserRoles.Infrasructure.Repositories;

namespace Tasko.BoardsService.Infrasructure.Functions
{
    public class RolesFunctions
    {
        internal static Func<IRoleRepository, IMapper, Guid, Task<IResult>> FindRole()
        {
            return [Authorize] async (IRoleRepository userRepository, IMapper mapper, Guid id) =>
            {
                return Results.Ok();
            };
        }

        internal static Func<IRoleRepository, IMapper, Task<IResult>> GetRoles()
        {
            return [Authorize] async (IRoleRepository repository, IMapper mapper) =>
            {
                return Results.Ok();
            };
        }

        internal static Func<IRoleRepository, IMapper, UserCreate, IValidator<IUserCreate>, Task<IResult>> CreateRole(JwtValidationParameter jwtValidationParameter)
        {
            return async (IRoleRepository userRepository, IMapper mapper, UserCreate userCreate, IValidator<IUserCreate> validator) =>
            {
                return Results.Ok();
            };
        }

        internal static Func<HttpContext, IRoleRepository, IMapper, UserUpdate, IValidator<IUserUpdate>, Task<IResult>> UpdateRole(JwtValidationParameter jwtValidationParmeter)
        {
            return [Authorize] async (HttpContext context, IRoleRepository userRepository, IMapper mapper, UserUpdate userUpdate, IValidator<IUserUpdate> validator) =>
            {

                return Results.Ok();
            };
        }

        internal static Func<HttpContext, IRoleRepository, IMapper, Guid, Task<IResult>> DeleteRole(JwtValidationParameter jwtValidationParmeter)
        {
            return [Authorize] async (HttpContext context, IRoleRepository userRepository, IMapper mapper, Guid id) =>
            {
                return Results.Ok();
            };
        }
    }
}