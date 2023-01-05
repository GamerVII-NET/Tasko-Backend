using FluentValidation.Results;
using System.Data;
using Tasko.Domains.Models.DTO.Role;
using Tasko.Domains.Models.RequestResponses;
using Tasko.Domains.Models.Structural;
using Tasko.Jwt.Services;

namespace Tasko.Service.Infrastructure.RequestHandlers;

internal static class RequestHandler
{
    internal static Func<IRoleRepository, ValidationParameter, IMapper, CancellationToken, Task<IResult>> GetRoles()
    {
        return [Authorize] async (IRoleRepository roleRepository, ValidationParameter jwtValidationParameter, IMapper mapper, CancellationToken cancellationToken) =>
        {
            List<RoleRead> endRoles = new List<RoleRead>();
            var roles = await roleRepository.GetAsync(cancellationToken);

            foreach (var findRole in roles)
            {
                IEnumerable<IPermission> rolePermissions = await roleRepository.GetRolePermissions(findRole, cancellationToken);
                var roleRead = mapper.Map<RoleRead>(findRole);
                roleRead.Permissions = rolePermissions;

                endRoles.Add(roleRead);
            }

            var rolesRead = mapper.Map<IEnumerable<RoleRead>>(roles);

            return Results.Ok(new GetRequestResponse<IRoleRead>(endRoles));
        };
    }


    internal static Func<IRoleRepository, IMapper, RoleCreate, IValidator<IRoleCreate>, CancellationToken, Task<IResult>> CreateRole(ValidationParameter validationParameter)
    {
        return [AllowAnonymous] async (IRoleRepository roleRepository, IMapper mapper, RoleCreate roleCreate, IValidator<IRoleCreate> validator, CancellationToken cancellationToken) =>
        {
            var validationResult = validator.Validate(roleCreate);

            if (!validationResult.IsValid)
            {
                var result = new BadRequestResponse<List<ValidationFailure>>(validationResult.Errors, "Validation error");
                return Results.BadRequest(result);
            }

            var role = mapper.Map<Role>(roleCreate);

            var newRole = await roleRepository.CreateAsync(role);
            var rolePermissions = await roleRepository.GetRolePermissions(role, cancellationToken);

            var roleRead = mapper.Map<RoleRead>(role);

            roleRead.Permissions = rolePermissions;

            return Results.Ok(roleRead);
        };
    }
}