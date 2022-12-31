using Tasko.Service.Infrastructure.Requests;

namespace Tasko.Service.Infrastructure.RouteHandlers;

public class UserRouteHandler : IRouteHandler<WebApplication>
{
    public void Initialzie(WebApplication webApplication) => Getters(webApplication);

    public void Getters(WebApplication webApplication) => webApplication.MapGet("api/users", RequestHandler.GetUsers())
                                                                        .Produces<IEnumerable<IUser>>(StatusCodes.Status200OK)
                                                                        .WithName("Get users")
                                                                        .WithTags("Getters");
}