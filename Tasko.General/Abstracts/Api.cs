using Tasko.General.Extensions.Jwt;

namespace Tasko.General.Abstracts
{
    public abstract class BaseRouteHandler
    {
        public WebApplication WebApplication { get; set; }
        public JwtValidationParameter JwtValidationParmeter { get { return WebApplication.Configuration.GetJwtValidationParameter(); } }
    }
}
