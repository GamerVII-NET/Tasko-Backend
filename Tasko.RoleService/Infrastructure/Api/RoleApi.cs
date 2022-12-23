using Tasko.RoleService.Infrasructure.Functions;

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

        WebApplication.MapGet("api/roles", RolesFunctions.GetRoles())
                      .WithName("Get all roles")
                      .WithTags("Getters");


        WebApplication.MapGet("api/users/{id}", RolesFunctions.FindRole())
                      .WithName("Get user by id")
                      .WithTags("Getters");

    }
    private void Creators()
    {
        WebApplication.MapPost("api/roles", RolesFunctions.CreateRole(JwtValidationParmeter))
                      .WithName("Create role")
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