using System.Security;
using Tasko.General.Abstracts;
using Tasko.General.Models.RequestResponses;
using Tasko.PermissionService.Infrastructure.Functions;

namespace Tasko.PermissionService.Infrastructure.Api
{
    public class PermissionApi : BaseRouteHandler, General.Interfaces.IRouteHandler
    {
        public void Register(WebApplication webApplication)
        {
            WebApplication = webApplication;
            Getters();
            Creators();
            Updaters();
            Deletters();
        }

        private void Getters()
        {
            WebApplication.MapGet("/api/permissions/", PermissionFuctions.GetPermissions())
                          .Produces<RequestResponse<IEnumerable<IPermission>>>(StatusCodes.Status200OK)
                          .WithName("Get permissions")
                          .WithTags("Getters");

            WebApplication.MapGet("/api/permissions/{id}", PermissionFuctions.FindPermission())
                          .Produces<RequestResponse<IPermission>>(StatusCodes.Status200OK)
                          .WithName("Find permission")
                          .WithTags("Getters");
        }

        private void Creators()
        {
            WebApplication.MapPost("/api/permission", PermissionFuctions.CreatePermission())
                          .Produces<RequestResponse<IPermission>>(StatusCodes.Status200OK)
                          .WithName("Create permission")
                          .WithTags("Creators");
        }

        private void Updaters()
        {
            WebApplication.MapPut("/api/permission", PermissionFuctions.UpdatePermission())
                          .Produces<RequestResponse<IPermission>>(StatusCodes.Status200OK)
                          .WithName("Update permission")
                          .WithTags("Updaters");
        }

        private void Deletters()
        {
            WebApplication.MapDelete("/api/permission", PermissionFuctions.DeletePermission())
                          .Produces<RequestResponse<DeleteResult>>(StatusCodes.Status200OK)
                          .WithName("Delete permission")
                          .WithTags("Deletters");
        }

    }
}
