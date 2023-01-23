using Tasko.Domains.Models.DTO.Role;
using Tasko.Service.Infrastructure.RequestHandlers;
using IRouteHandler = Tasko.Domains.Interfaces.IRouteHandler;

namespace Tasko.Service.Infrastructure.RouteHandlers;

public class RouteHandler : IRouteHandler
{
    private static WebApplication _webApplication;

    public void Initialzie(WebApplication webApplication)
    {
        _webApplication = webApplication;
        Getters();
        Creators();
        Updaters();
        Deleters();
    }


    public void Getters()
    {
        _webApplication.MapGet("api/roles", RequestHandler.GetRoles())
                       .Produces<IEnumerable<IRoleRead>>(StatusCodes.Status200OK)
                       .WithName("Get roles")
                       .WithTags("Getters");

        _webApplication.MapGet("api/roles/{id}", RequestHandler.FindRole())
                       .Produces<IRoleRead>(StatusCodes.Status200OK)
                       .WithName("Find role")
                       .WithTags("Getters");



    }
    public void Creators()
    {
        _webApplication.MapPost("api/roles", RequestHandler.CreateRole())
                       .Produces<IRoleRead>(StatusCodes.Status201Created)
                       .WithName("Create role")
                       .WithTags("Creators");


    }
    private void Updaters()
    {
        _webApplication.MapPut("api/roles", RequestHandler.UpdateRole())
                       .Produces<IRoleRead>(StatusCodes.Status201Created)
                       .WithName("Update role")
                       .WithTags("Updaters");
    }
    private void Deleters()
    {
        _webApplication.MapDelete("api/roles", RequestHandler.DeleteRole())
                       .Produces<IRoleRead>(StatusCodes.Status201Created)
                       .WithName("Delete role")
                       .WithTags("Deleters");
    }
}