using Tasko.Domains.Interfaces;
using Tasko.BoardSevice.Infrasructure.Functions;
using IRouteHandler = Tasko.Domains.Interfaces.IRouteHandler;

namespace Tasko.BoardSevice.Infrasructure.Api;

public class UserApi : RouteHandlerBase, IRouteHandler
{
    public void Register(WebApplication webApplication)
    {
        Getters(webApplication);
        Creators(webApplication);
        Updaters(webApplication);
        Deleters(webApplication);
    }
    private void Getters(WebApplication webApplication)
    {

        WebApplication.MapGet("api/boards/{id}", BoardsFunctions.FindBoard())
            .WithName("Get board by id")
            .WithTags("Getters");

        WebApplication.MapGet("api/boards/", BoardsFunctions.GetBoards())
            .WithName("Get all boards")
            .WithTags("Getters");

    }
    private void Creators(WebApplication webApplication)
    {
        WebApplication.MapPost("api/boards", BoardsFunctions.CreateBoard(JwtValidationParmeter))
            .WithName("Create board")
            .WithTags("Creators");
    }
    private void Updaters(WebApplication webApplication)
    {
        WebApplication.MapPut("api/boards", BoardsFunctions.UpdateBoard(JwtValidationParmeter))
            .WithName("Update board")
            .WithTags("Updaters");
    }
    private void Deleters(WebApplication webApplication)
    {
        WebApplication.MapDelete("api/boards", BoardsFunctions.DeleteRole(JwtValidationParmeter))
            .WithName("Delete board")
            .WithTags("Deleters");
    }
}