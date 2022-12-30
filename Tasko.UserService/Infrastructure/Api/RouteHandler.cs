using Tasko.Domains.Models.Structural.Providers;
using Tasko.General.Abstracts;
using Tasko.Service.Infrastructure.Requests;

namespace Tasko.Service.Infrastructure.Api;

public class RouteHandler : BaseRouteHandler, IRouteHandler
{
    public void Register(WebApplication webApplication)
    {
        WebApplication = webApplication;
        Getters();
        //Creators();
        //Updaters();
        //Deleters();
    }

    private void Getters()
    {
        WebApplication.MapGet("api/users", RequestHandler.GetUsers())
            .Produces<IEnumerable<IUser>>(StatusCodes.Status200OK)
            .WithName("Get users")
            .WithTags("Getters");
    }
}