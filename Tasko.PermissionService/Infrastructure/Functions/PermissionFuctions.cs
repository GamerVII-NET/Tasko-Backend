using Tasko.Domains.Models.DTO.Permissions;
using Tasko.Domains.Models.Structural.Providers;
using Tasko.General.Models.RequestResponses;
using Tasko.PermissionService.Infrastructure.Repositories;

namespace Tasko.PermissionService.Infrastructure.Functions
{
    internal class PermissionFuctions
    {
        internal static Func<IPermissionRepository, Guid, Task<IResult>> FindPermission()
        {
            return [Authorize] async (IPermissionRepository permissionRepository, Guid id) =>
            {
                var permission = await permissionRepository.FindPermissionAsync(id);
                return permission == null ?
                Results.NotFound(new BadRequestResponse<Guid>(id, "Permission not found", StatusCodes.Status404NotFound)) :
                Results.Ok(new RequestResponse<IPermission>(permission, StatusCodes.Status302Found));
            };
        }
        internal static Func<IPermissionRepository, Guid, Task<IResult>> GetPermissions() =>
                  [Authorize] async (IPermissionRepository permissionRepository, Guid id) =>
                  Results.Ok(new RequestResponse<IEnumerable<IPermission>>(await permissionRepository.GetPermissionsAsync(), StatusCodes.Status200OK));
        internal static Func<IPermissionRepository, IPermissionCreate, IMapper, Task<IResult>> CreatePermission()
        {
            return [Authorize] async (IPermissionRepository permissionRepository, IPermissionCreate permissionCreate, IMapper mapper) =>
            {
                var foundPermission = await permissionRepository.FindPermissionAsync(permissionCreate.Name);
                if (foundPermission != null)
                    return Results.Conflict(new BadRequestResponse<IPermissionCreate>(permissionCreate, "Permission already exsist", StatusCodes.Status409Conflict));
                var permission = mapper.Map<Permission>(permissionCreate);
                permission.CreatedAt = DateTime.Now;
                await permissionRepository.CreatePermissionAsync(permission);
                return Results.Created($"api/permission/{permission.Id}", new RequestResponse<IPermission>(permission, StatusCodes.Status201Created));
            };
        }
        internal static Func<IPermissionRepository, IPermissionUpdate, IMapper, Task<IResult>> UpdatePermission()
        {
            return [Authorize] async (IPermissionRepository permissionRepository, IPermissionUpdate permissionUpdate, IMapper mapper) =>
            {
                var foundPermission = await permissionRepository.FindPermissionAsync(permissionUpdate.Name) as Permission;
                if (foundPermission == null)
                    return Results.NotFound(new BadRequestResponse<IPermissionUpdate>(permissionUpdate, "Permission not exsist", StatusCodes.Status404NotFound));
                var permission = mapper.Map<Permission>(permissionUpdate);
                permission.UpdatedAt = DateTime.Now;
                await permissionRepository.UpdatePermissionAsync(foundPermission, permission);
                return Results.Ok(new RequestResponse<IPermission>(permission, StatusCodes.Status202Accepted));
            };
        }
        internal static Func<IPermissionRepository, Guid, Task<IResult>> DeletePermission()
        {
            return [Authorize] async (IPermissionRepository permissionRepository, Guid id) =>
            {
                var permission = await permissionRepository.FindPermissionAsync(id);
                if (permission == null)
                    return Results.NotFound(new BadRequestResponse<Guid>(id, "Permission not found", StatusCodes.Status404NotFound));
                var result = await permissionRepository.DeletePermissionAsync(id);
                return Results.Ok(new RequestResponse<DeleteResult>(result, StatusCodes.Status200OK));

            };
        }
    }
}
