using Tasko.Service.Infrastructure.RequestHandlers;

namespace Tasko.Service.Infrastructure.RouteHandlers;

public class UserRouteHandler : IRouteHandler<WebApplication>
{
    private ValidationParameter _validationParameter;
    private WebApplication _webApplication;

    public void Initialzie(WebApplication webApplication)
    {
        _validationParameter = webApplication.Configuration.GetValidationParameter();
        _webApplication = webApplication;
        Getters();
        Creators();
        Updaters();
        Deleters();
    }

    public void Getters()
    {
        _webApplication.MapGet("api/users", UserRequestHandler.GetUsers())
                       .Produces<IEnumerable<IUser>>(StatusCodes.Status200OK)
                       .WithName("Get users")
                       .WithTags("Getters");

        _webApplication.MapGet("api/users/{id}", UserRequestHandler.FindUser())
                       .Produces<IUser>(StatusCodes.Status200OK)
                       .WithName("Find user")
                       .WithTags("Getters");

        _webApplication.MapGet("api/users/refresh-tokens/{id}", UserRequestHandler.GetRefreshTokens(_validationParameter))
                       .Produces<IEnumerable<IRefreshToken>>(StatusCodes.Status200OK)
                       .WithName("Get user refres tokens")
                       .WithTags("Getters");


    }
    public void Creators()
    {
        _webApplication.MapPost("api/users", UserRequestHandler.CreateUser(_validationParameter))
                       .Produces<IEnumerable<IUser>>(StatusCodes.Status200OK)
                       .WithName("Create user")
                       .WithTags("Creators");


    }
    private void Updaters()
    {
        _webApplication.MapPut("api/users", UserRequestHandler.UpdateUser(_validationParameter))
                       .Produces(StatusCodes.Status200OK)
                       .WithName("Update user")
                       .WithTags("Updaters");
    }
    private void Deleters()
    {
        _webApplication.MapDelete("api/users", UserRequestHandler.DeleteUser(_validationParameter))
                       .Produces<IEnumerable<IUser>>(StatusCodes.Status200OK)
                       .WithName("Delete user")
                       .WithTags("Deleters");
    }
}