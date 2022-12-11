using Microsoft.Extensions.Configuration;
using Tasko.General.Extensions.Crypthography;

namespace Tasko.General.Extensions.Jwt
{
    public static class JwtValidationParameterExtension
    {
        public static JwtValidationParameter GetJwtValidationParameter(this IConfiguration configuration) =>
        new JwtValidationParameter { Key = configuration.GetKey(), Audienece = configuration.GetAudience(), Issuer = configuration.GetIssuer() };
    }

}

