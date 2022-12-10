using Tasko.AuthService.Infrastructure.Functions;
using Tasko.Domains.Models.Structural.Providers;
using Tasko.General.Abstracts;
using Tasko.General.Interfaces;

namespace Tasko.AuthService.Infrastructure.Api
{
    public class AuthApi : ApiBase, IApi
    {
        public void Register(WebApplication webApplication)
        {
            WebApplication = webApplication;
            Auth(webApplication);
        }

        private void Auth(WebApplication webApplication)
        {
            webApplication.MapPost("api/authorization", AuthFunctions.BearerAuthorization(JwtValidationParmeter))
                          .Produces<IEnumerable<IUser>>(StatusCodes.Status200OK)
                          .WithName("Authorization")
                          .WithTags("Auth");
        }
    }
}
