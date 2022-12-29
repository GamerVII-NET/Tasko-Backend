using Tasko.BoardSevice.Infrasructure.Functions;

namespace Tasko.BoardSevice.Infrasructure.Api;

public class UserApi : BaseRouteHandler, General.Interfaces.IRouteHandler
{
    public void Register(WebApplication webApplication)
    {
        WebApplication = webApplication;
        Getters();
        Creators();
        Updaters();
        Deleters();
    }
    private void Getters()
    {

        WebApplication.MapGet("api/boards/{id}", BoardsFunctions.FindBoard())
            .WithName("Get board by id")
            .WithTags("Getters");

        WebApplication.MapGet("api/boards/", BoardsFunctions.GetBoards())
            .WithName("Get all boards")
            .WithTags("Getters");

    }
    private void Creators()
    {
        WebApplication.MapPost("api/boards", BoardsFunctions.CreateBoard(JwtValidationParmeter))
            .WithName("Create board")
            .WithTags("Creators");
    }
    private void Updaters()
    {
        WebApplication.MapPut("api/boards", BoardsFunctions.UpdateBoard(JwtValidationParmeter))
            .WithName("Update board")
            .WithTags("Updaters");
    }
    private void Deleters()
    {
        WebApplication.MapDelete("api/boards", BoardsFunctions.DeleteRole(JwtValidationParmeter))
            .WithName("Delete board")
            .WithTags("Deleters");
    }
}