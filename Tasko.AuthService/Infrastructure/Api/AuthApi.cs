using Tasko.AuthService.Infrastructure.Functions;

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
