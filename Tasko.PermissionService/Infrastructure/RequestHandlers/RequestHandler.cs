using FluentValidation.Results;
using Tasko.Domains.Models.DTO.Permissions;
using Tasko.Domains.Models.RequestResponses;

namespace Tasko.Service.Infrastructure.RequestHandlers
{
    internal static class RequestHandler
    {

        internal static Func<HttpContext, IPermissionRepository, ValidationParameter, IMapper, CancellationToken, Guid, Task<IResult>> DeletePermission()
        {
            return [Authorize] async (HttpContext context, IPermissionRepository permissionRepository, ValidationParameter jwtValidationParameter, IMapper mapper, CancellationToken cancellationToken, Guid id) =>
            {
                var UserId = context.Items["UserId"];

                if (UserId == null)
                    return Results.Unauthorized();

                var findPermission = await permissionRepository.FindOneAsync(id);

                if (findPermission == null)
                {
                    var result = new List<ValidationFailure>
                    {
                        new ValidationFailure("Permission", $"Permission with unique identificator {id} not found", "User not found")
                    };

                    return Results.NotFound(new BadRequestResponse<List<ValidationFailure>>(result, "Permission not found", StatusCodes.Status404NotFound));
                }

                var deletedPermission = await permissionRepository.DeleteAsync(id, cancellationToken);

                return Results.Ok();
            };
        }

        internal static Func<HttpContext, IPermissionRepository, ValidationParameter, IMapper, CancellationToken, Task<IResult>> GetPermissions()
        {
            return [Authorize] async (HttpContext context, IPermissionRepository permissionRepository, ValidationParameter jwtValidationParameter, IMapper mapper, CancellationToken cancellationToken) =>
            {
                var UserId = context.Items["UserId"];

                if (UserId == null)
                    return Results.Unauthorized();

                var permissions = await permissionRepository.GetAsync(cancellationToken);

                var permissionsRead = mapper.Map<IEnumerable<PermissionRead>>(permissions);

                return Results.Ok(new GetRequestResponse<IPermissionRead>(permissionsRead));
            };
        }


        internal static Func<HttpContext, IPermissionRepository, IMapper, PermissionCreate, IValidator<IPermissionCreate>, CancellationToken, Task<IResult>> CreatePermission(ValidationParameter validationParameter)
        {
            return [Authorize] async (HttpContext context, IPermissionRepository permissionRepository, IMapper mapper, PermissionCreate permissionCreate, IValidator<IPermissionCreate> validator, CancellationToken cancellationToken) =>
            {
                var UserId = context.Items["UserId"];

                if (UserId == null)
                    return Results.Unauthorized();

                var validationResult = validator.Validate(permissionCreate);

                if (!validationResult.IsValid)
                {
                    var result = new BadRequestResponse<List<ValidationFailure>>(validationResult.Errors, "Validation error");
                    return Results.BadRequest(result);
                }

                var findPermission = await permissionRepository.FindOneAsync(c => c.Name.Equals(permissionCreate.Name));

                if (findPermission != null)
                {
                    return Results.Conflict(new BadRequestResponse<IPermissionCreate>(permissionCreate, "Permission already exsist", StatusCodes.Status409Conflict));
                }

                var permission = mapper.Map<Permission>(permissionCreate);

                var newPermission = await permissionRepository.CreateAsync(permission);

                var permissionRead = mapper.Map<PermissionRead>(permission);

                return Results.Created($"/api/permissions/{permissionRead.Id}", new RequestResponse<IPermissionRead>(permissionRead, StatusCodes.Status201Created));
            };
        }
    }
}