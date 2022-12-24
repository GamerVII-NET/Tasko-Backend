using Tasko.General.Extensions.Jwt;

namespace Tasko.General.Abstracts
{
    public abstract class ApiBase
    {
        public WebApplication WebApplication { get; set; }
        public JwtValidationParameter JwtValidationParmeter { get { return WebApplication.Configuration.GetJwtValidationParameter(); } }
    }
}
