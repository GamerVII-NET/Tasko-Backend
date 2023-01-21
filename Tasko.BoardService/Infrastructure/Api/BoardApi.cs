using Tasko.Domains.Interfaces;
using Tasko.BoardSevice.Infrasructure.Functions;
using IRouteHandler = Tasko.Domains.Interfaces.IRouteHandler;
using Tasko.Jwt.Models;
using Tasko.Jwt.Extensions;

namespace Tasko.BoardSevice.Infrasructure.Api;

public class UserApi : IRouteHandler
{
    private WebApplication _webApplication;
    private ValidationParameter _validationParameter;

    public void Initialzie(WebApplication webApplication)
    {
        _webApplication = webApplication;
        _validationParameter = webApplication.Configuration.GetValidationParameter();
    }

    private void Getters(WebApplication webApplication)
    {

        _webApplication.MapGet("api/boards/{id}", BoardsFunctions.FindBoard())
            .WithName("Get board by id")
            .WithTags("Getters");

        _webApplication.MapGet("api/boards/", BoardsFunctions.GetBoards())
            .WithName("Get all boards")
            .WithTags("Getters");

    }
    private void Creators(WebApplication webApplication)
    {
        _webApplication.MapPost("api/boards", BoardsFunctions.CreateBoard(_validationParameter))
            .WithName("Create board")
            .WithTags("Creators");
    }
    private void Updaters(WebApplication webApplication)
    {
        _webApplication.MapPut("api/boards", BoardsFunctions.UpdateBoard(_validationParameter))
            .WithName("Update board")
            .WithTags("Updaters");
    }
    private void Deleters(WebApplication webApplication)
    {
        _webApplication.MapDelete("api/boards", BoardsFunctions.DeleteRole(_validationParameter))
            .WithName("Delete board")
            .WithTags("Deleters");
    }
}