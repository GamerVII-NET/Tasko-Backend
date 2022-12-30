using Tasko.Domains.Models.Structural;
using Tasko.RoleService.Infrasructure.Repositories;

namespace Tasko.RoleService.Infrasructure.Functions;

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
            var roles = await repository.GetRolesAsync();

            var response = new GetRequestResponse<RoleRead>(mapper.Map<IEnumerable<IRole>, IEnumerable <RoleRead>>(roles));

            return Results.Ok(response);
        };
    }

    internal static Func<IRoleRepository, IMapper, RoleCreate, IValidator<IRoleCreate>, Task<IResult>> CreateRole(JwtValidationParameter jwtValidationParameter)
    {
        return async (IRoleRepository roleRepository, IMapper mapper, RoleCreate roleCreate, IValidator<IRoleCreate> validator) =>
        {
            var validate = validator.Validate(roleCreate);

            if (!validate.IsValid)
            {
                return Results.BadRequest(new BadRequestResponse<List<ValidationFailure>>(validate.Errors, "Ошибка валидации данных"));
            }

            var role = mapper.Map<RoleCreate, Role>(roleCreate);

            var newRole = await roleRepository.CreateRole(role);

            var response = new RequestResponse<IRole>(newRole, StatusCodes.Status201Created);

            return Results.Created($"/Roles/{newRole.Id}", response);

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