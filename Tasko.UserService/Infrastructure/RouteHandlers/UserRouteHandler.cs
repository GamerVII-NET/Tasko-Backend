using Tasko.Service.Infrastructure.Requests;

namespace Tasko.Service.Infrastructure.RouteHandlers;

public class UserRouteHandler : IRouteHandler<WebApplication>
{
    public void Initialzie(WebApplication webApplication)
    {
        Getters(webApplication);
        Creates(webApplication);
    }

    private void Creates(WebApplication webApplication)
    {
        webApplication.MapPost("api/users", RequestHandler.CreateUser())
            .Produces<IEnumerable<IUser>>(StatusCodes.Status200OK)
            .WithName("Create user")
            .WithTags("Creates");
    }

    public void Getters(WebApplication webApplication)
    {
        webApplication.MapGet("api/users", RequestHandler.GetUsers())
            .Produces<IEnumerable<IUser>>(StatusCodes.Status200OK)
            .WithName("Get users")
            .WithTags("Getters");


    }
}