using Tasko.Domains.Models.Structural.Providers;
using Tasko.UserService.Infrasructure.Functions;

namespace Tasko.UserService.Infrasructure.Api
{
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
            WebApplication.MapGet("api/users", UserFunctions.GetUsers())
                          .Produces<IEnumerable<IUser>>(StatusCodes.Status200OK)
                          .WithName("Get users")
                          .WithTags("Getters");

            WebApplication.MapGet("api/users/{id}", UserFunctions.FindUser())
                          .Produces<IEnumerable<IUser>>(StatusCodes.Status200OK)
                          .WithName("Get user by id")
                          .WithTags("Getters");

        }
        private void Creators()
        {
            WebApplication.MapPost("api/users", UserFunctions.CreateUser(JwtValidationParmeter))
                          .Produces(StatusCodes.Status200OK)
                          .WithName("Create user")
                          .WithTags("Creators");
        }
        private void Updaters()
        {
            WebApplication.MapPut("api/users", UserFunctions.UpdateUser(JwtValidationParmeter))
                          .Produces(StatusCodes.Status200OK)
                          .WithName("Update user")
                          .WithTags("Updaters");
        }
        private void Deleters()
        {
            WebApplication.MapDelete("api/users", UserFunctions.DeleteUser(JwtValidationParmeter))
                          .Produces<IEnumerable<IUser>>(StatusCodes.Status200OK)
                          .WithName("Delete user")
                          .WithTags("Deleters");
        }
    }
}
