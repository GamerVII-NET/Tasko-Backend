using FluentValidation.Results;
using Tasko.Domains.Models.DTO.Role;
using Tasko.Domains.Models.RequestResponse;
using Tasko.Domains.Models.RequestResponses;

namespace Tasko.Service.Infrastructure.RequestHandlers;

internal static class RequestHandler
{
    internal static Func<IRoleRepository, IMapper, CancellationToken, Task<IResult>> GetRoles()
    {
        return [AllowAnonymous] async (IRoleRepository roleRepository, IMapper mapper, CancellationToken cancellationToken) =>
        {
            var roles = await roleRepository.GetAsync(cancellationToken);
            var rolesRead = mapper.Map<IEnumerable<RoleRead>>(roles);
            return Results.Ok(new RequestResponseCollection<RoleRead>(rolesRead));
        };
    }
    internal static Func<IRoleRepository, Guid, IMapper, CancellationToken, Task<IResult>> FindRole()
    {
        return [Authorize] async (IRoleRepository roleRepository, Guid id, IMapper mapper, CancellationToken cancellationToken) =>
        {
            var roles = await roleRepository.FindOneAsync(id, cancellationToken);
            var roleRead = mapper.Map<IRoleRead>(roles);
            return Results.Ok(new RequestResponse<IRoleRead>(roleRead, 200));
        };
    }
    internal static Func<IRoleRepository, IMapper, RoleCreate, IValidator<IRoleCreate>, CancellationToken, Task<IResult>> CreateRole()
    {
        return [Authorize] async (IRoleRepository roleRepository, IMapper mapper, RoleCreate roleCreate, IValidator<IRoleCreate> validator, CancellationToken cancellationToken) =>
        {
            var validationResult = validator.Validate(roleCreate);
            if (!validationResult.IsValid)
            {
                var result = new BadRequestResponse<List<ValidationFailure>>(validationResult.Errors, "Validation error");
                return Results.BadRequest(result);
            }
            var role = mapper.Map<Role>(roleCreate);
            var createdRole = await roleRepository.CreateAsync(role, cancellationToken);
            var roleRead = mapper.Map<RoleRead>(createdRole);
            return Results.Created($"api/roles/{role.Id}", new RequestResponse<RoleRead>(roleRead, 201));
        };
    }
    internal static Func<IRoleRepository, IMapper, RoleUpdate, IValidator<IRoleUpdate>, CancellationToken, Task<IResult>> UpdateRole()
    {
        return [Authorize] async (IRoleRepository roleRepository, IMapper mapper, RoleUpdate roleUpdate, IValidator<IRoleUpdate> validator, CancellationToken cancellationToken) =>
        {
            var validationResult = validator.Validate(roleUpdate);
            if (!validationResult.IsValid)
            {
                var result = new BadRequestResponse<List<ValidationFailure>>(validationResult.Errors, "Validation error");
                return Results.BadRequest(result);
            }
            var role = mapper.Map<Role>(roleUpdate);
            var createdRole = await roleRepository.UpdateAsync(role, cancellationToken);
            var roleRead = mapper.Map<RoleRead>(createdRole);
            return Results.Ok(new RequestResponse<RoleRead>(roleRead, 200));
        };
    }
    internal static Func<IRoleRepository, IMapper, Guid, CancellationToken, Task<IResult>> DeleteRole()
    {
        return [Authorize] async (IRoleRepository roleRepository, IMapper mapper, Guid id, CancellationToken cancellationToken) =>
        {
            var role = roleRepository.DeleteAsync(id, cancellationToken);
            var roleRead = mapper.Map<RoleRead>(role);
            return Results.Ok(new RequestResponse<RoleRead>(roleRead, 200));
        };
    }
}