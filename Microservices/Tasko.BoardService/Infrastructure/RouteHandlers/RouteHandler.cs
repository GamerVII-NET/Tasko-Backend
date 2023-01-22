using Tasko.BoardSevice.Infrasructure.Functions;
using Tasko.Jwt.Extensions;
using Tasko.Jwt.Models;
using IRouteHandler = Tasko.Domains.Interfaces.IRouteHandler;

namespace Tasko.BoardSevice.Infrasructure.RouteHandlers;

public class RouteHandler : IRouteHandler
{
    private static ValidationParameter ValidationParameter;
    public void Initialzie(WebApplication webApplication)
    {
        Getters(webApplication);
        Creators(webApplication);
        Updaters(webApplication);
        Deleters(webApplication);
        ValidationParameter = webApplication.Configuration.GetValidationParameter();
    }
    private void Getters(WebApplication webApplication)
    {

        webApplication.MapGet("api/boards/{id}", RequestHandler.FindBoard())
            .WithName("Get board by id")
            .WithTags("Getters");

        webApplication.MapGet("api/boards/", RequestHandler.GetBoards())
            .WithName("Get all boards")
            .WithTags("Getters");

    }
    private void Creators(WebApplication webApplication)
    {
        webApplication.MapPost("api/boards", RequestHandler.CreateBoard(ValidationParameter))
            .WithName("Create board")
            .WithTags("Creators");
    }
    private void Updaters(WebApplication webApplication)
    {
        webApplication.MapPut("api/boards", RequestHandler.UpdateBoard(ValidationParameter))
            .WithName("Update board")
            .WithTags("Updaters");
    }
    private void Deleters(WebApplication webApplication)
    {
        webApplication.MapDelete("api/boards", RequestHandler.DeleteRole(ValidationParameter))
            .WithName("Delete board")
            .WithTags("Deleters");
    }
}