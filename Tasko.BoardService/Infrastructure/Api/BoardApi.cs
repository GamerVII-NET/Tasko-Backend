using Tasko.BoardSevice.Infrasructure.Functions;
using Tasko.Domains.Models.Structural.Providers;
using Tasko.General.Abstracts;
using Tasko.General.Interfaces;
using Tasko.General.Models;

public class UserApi : ApiBase, IApi
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