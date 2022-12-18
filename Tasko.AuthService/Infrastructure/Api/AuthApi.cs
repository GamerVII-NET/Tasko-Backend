using Tasko.AuthService.Infrastructure.Functions;
using Tasko.Domains.Models.DTO.User;
using Tasko.General.Abstracts;
using Tasko.General.Interfaces;
using Tasko.General.Models.RequestResponses;

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
            webApplication.MapPost("api/auth", AuthFunctions.BearerAuthorization(JwtValidationParmeter))
                          .Produces<IRequestResponse<IUserAuthRead>>(StatusCodes.Status200OK)
                          .WithName("Authorization")
                          .WithTags("Auth");
        }
    }
}
