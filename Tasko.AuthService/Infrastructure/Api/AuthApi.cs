using Tasko.AuthService.Infrastructure.Functions;
using Tasko.Domains.Interfaces;

namespace Tasko.AuthService.Infrastructure.Api
{
    public class AuthApi : IRouteHandler<WebApplication>
    {
        public void Initialzie(WebApplication webApplication)
        {
            Auth(webApplication);
        }

        private void Auth(WebApplication webApplication)
        {
            webApplication.MapPost("api/auth", AuthFunctions.BearerAuthorization(JwtValidationParmeter))
                          .Produces<IRequestResponse<IUserAuthRead>>(StatusCodes.Status200OK)
                          .WithName("Authorization")
                          .WithTags("Auth");

            webApplication.MapPost("api/auth/refresh-token", AuthFunctions.RefreshTokenAuthorization(JwtValidationParmeter))
                          .Produces<IRequestResponse<IUserAuthRead>>(StatusCodes.Status200OK)
                          .WithName("Refresh token authorization")
                          .WithTags("Auth");
        }
    }
}
