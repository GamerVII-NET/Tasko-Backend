using Tasko.General.Extensions.Jwt;

namespace Tasko.General.Abstracts
{
    public abstract class ApiBase
    {
        public required WebApplication WebApplication { get; set; }
        public JwtValidationParameter JwtValidationParmeter { get { return WebApplication.Configuration.GetJwtValidationParameter(); } }
    }
}
