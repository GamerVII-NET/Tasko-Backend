using Tasko.Domains.Models.Structural.Interfaces;
using Tasko.Server.Infrastructure.API.Interfaces;
using Tasko.Server.Infrastructure.Services;

namespace Tasko.Server.Infrastructure.API.Providers
{
    public class AuthApi : IApi
    {
        public void Register(WebApplication webApplication)
        {
            Auth(webApplication);
        }

        private void Auth(WebApplication webApplication)
        {
            string key = webApplication.Configuration["Jwt:Key"],
                   issuer = webApplication.Configuration["Jwt:Issuer"],
                   audience = webApplication.Configuration["Jwt:Audience"];

            webApplication.MapPost("api/authorization", AuthService.BearerAuthorization(key, issuer, audience))
                          .Produces<IEnumerable<IUser>>(StatusCodes.Status200OK)
                          .WithName("Authorization")
                          .WithTags("Auth");
        }
    }
}
