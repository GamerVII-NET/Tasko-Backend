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
        }

        private void Creators(WebApplication webApplication)
        {
            string key = webApplication.Configuration["Jwt:Key"];
            string issuer = webApplication.Configuration["Jwt:Issuer"];
            webApplication.MapPost("api/users", UserService.CreateUser(key, issuer))
                          .Produces<IUser>(StatusCodes.Status200OK)
                          .WithName("Create user")
                          .WithTags("Creators");
        }


    }
}
