using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Tasko.Domains.Models.DTO.Permissions;
using Tasko.Domains.Models.DTO.Role;
using Tasko.Domains.Models.RequestResponses;
using Tasko.Jwt.Services;

namespace Tasko.Service.Infrastructure.RequestHandlers
{
    internal static class RequestHandler
    {
        internal static Func<HttpContext, IPermissionService, ValidationParameter, IMapper, CancellationToken, Task<IResult>> GetPermissions()
        {
            return [Authorize] async (HttpContext context, IPermissionService permissionService, ValidationParameter jwtValidationParameter, IMapper mapper, CancellationToken cancellationToken) =>
            {
                var UserId = context.Items["UserId"];

                if (UserId == null)
                    return Results.Unauthorized();

                var permissions = await permissionService.GetAsync(cancellationToken);

                var permissionsRead = mapper.Map<IEnumerable<IPermissionRead>>(permissions);

                return Results.Ok(new RequestResponseCollection<IPermissionRead>(permissionsRead));
            };
        }


        internal static Func<IPermissionService, IMapper, RoleCreate, IValidator<IRoleCreate>, CancellationToken, Task<IResult>> CreatePermission(ValidationParameter validationParameter)
        {
            return [Authorize] async (IPermissionService roleRepository, IMapper mapper, RoleCreate roleCreate, IValidator<IRoleCreate> validator, CancellationToken cancellationToken) =>
            {
                var validationResult = validator.Validate(roleCreate);

                if (!validationResult.IsValid)
                {
                    var result = new BadRequestResponse<List<ValidationFailure>>(validationResult.Errors, "Validation error");
                    return Results.BadRequest(result);
                }

                return Results.Ok();
            };
        }
    }
}