using Tasko.Domains.Models.Structural.Interfaces;
using Tasko.Server.Infrastructure.API.Interfaces;

namespace Tasko.Server.Infrastructure.API.Providers
{
    public class UserApi : IApi
    {
        public void Register(WebApplication webApplication)
        {
            Getters(webApplication);
            Creators(webApplication);
        }

        private void Getters(WebApplication webApplication)
        {
            webApplication.MapGet("api/users", UserService.GetUsers())
                          .Produces<IEnumerable<IUser>>(StatusCodes.Status200OK)
                          .WithName("Get users")
                          .WithTags("Getters");

            webApplication.MapGet("api/users/{id}", UserService.FindUser())
                          .Produces<IEnumerable<IUser>>(StatusCodes.Status200OK)
                          .WithName("Get user by id")
                          .WithTags("Getters");

        }

        private void Creators(WebApplication webApplication)
        {
            string key = webApplication.Configuration["Jwt:Key"];
            string issuer = webApplication.Configuration["Jwt:Issuer"];
            string audience = webApplication.Configuration["Jwt:Audience"];
            webApplication.MapPost("api/users", UserService.CreateUser(key, issuer, audience))
                          .Produces(StatusCodes.Status200OK)
                          .WithName("Create user")
                          .WithTags("Creators");
        }


    }
}
