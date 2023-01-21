using Tasko.AuthService.Infrastructure.RequestHandlers;
using Tasko.Domains.Models.RequestResponses;
using Tasko.Jwt.Extensions;
using Tasko.Jwt.Models;
using IRouteHandler = Tasko.Domains.Interfaces.IRouteHandler;

namespace Tasko.AuthService.Infrastructure.RouteHandlers
{
    public class AuthRouteHandler : IRouteHandler
    {
        private WebApplication _webApplication;
        private ValidationParameter _validationParameter;
        public void Initialzie(WebApplication webApplication)
        {
            _webApplication = webApplication;
            _validationParameter = webApplication.Configuration.GetValidationParameter();

            Auth();
        }

        private void Auth()
        {
            _webApplication.MapPost("api/auth", AuthRequestHandler.BearerAuthorization(_validationParameter))
                          .Produces<IRequestResponse<IUserAuthRead>>(StatusCodes.Status200OK)
                          .WithName("Authorization")
                          .WithTags("Auth");

            _webApplication.MapPost("api/auth/refresh-token", AuthRequestHandler.RefreshTokenAuthorization(_validationParameter))
                          .Produces<IRequestResponse<IUserAuthRead>>(StatusCodes.Status200OK)
                          .WithName("Refresh token authorization")
                          .WithTags("Auth");
        }
    }
}
