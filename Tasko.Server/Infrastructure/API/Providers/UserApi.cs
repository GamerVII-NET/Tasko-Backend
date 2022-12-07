using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Tasko.Domains.Models.Structural.Interfaces;
using Tasko.Server.Infrastructure.API.Interfaces;
using Tasko.Server.Infrastructure.Extensions.AES;
using Tasko.Server.Infrastructure.Services;

namespace Tasko.Server.Infrastructure.API.Providers
{
    public class UserApi : IApi
    {
        public void Register(WebApplication webApplication)
        {
            Getters(webApplication);
            Creators(webApplication);
            Updaters(webApplication);
            Deletions(webApplication);
        }

        private void Deletions(WebApplication webApplication)
        {
            webApplication.MapDelete("api/users", UserService.DeleteUser(webApplication.Configuration))
                          .Produces<IEnumerable<IUser>>(StatusCodes.Status200OK)
                          .WithName("Delete user")
                          .WithTags("Deletions");
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

        private void Updaters(WebApplication webApplication)
        {
            webApplication.MapPut("api/users", UserService.UpdateUser(webApplication.Configuration))
                          .Produces(StatusCodes.Status200OK)
                          .WithName("Update user")
                          .WithTags("Updaters");
        }

        private void Creators(WebApplication webApplication)
        {
            string key = webApplication.Configuration["Jwt:Key"].Decrypt(AesService.AesKey, AesService.IV);
            string issuer = webApplication.Configuration["Jwt:Issuer"].Decrypt(AesService.AesKey, AesService.IV);
            string audience = webApplication.Configuration["Jwt:Audience"].Decrypt(AesService.AesKey, AesService.IV);
            webApplication.MapPost("api/users", UserService.CreateUser(key, issuer, audience))
                          .Produces(StatusCodes.Status200OK)
                          .WithName("Create user")
                          .WithTags("Creators");
        }


    }
}
