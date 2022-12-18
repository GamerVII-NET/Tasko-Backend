using Tasko.BoardsService.Infrasructure.Functions;
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

        WebApplication.MapGet("api/users/{id}", RolesFunctions.FindRole())
        .WithName("Get user by id")
                      .WithTags("Getters");

    }
    private void Creators()
    {
        WebApplication.MapPost("api/users", RolesFunctions.CreateRole(JwtValidationParmeter))
        .WithName("Create user")
                      .WithTags("Creators");
    }
    private void Updaters()
    {
        WebApplication.MapPut("api/users", RolesFunctions.UpdateRole(JwtValidationParmeter))
        .WithName("Update user")
                      .WithTags("Updaters");
    }
    private void Deleters()
    {
        WebApplication.MapDelete("api/users", RolesFunctions.DeleteRole(JwtValidationParmeter))
                      .WithName("Delete user")
                      .WithTags("Deleters");
    }
}