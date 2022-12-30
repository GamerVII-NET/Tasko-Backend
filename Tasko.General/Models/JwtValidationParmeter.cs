
using Tasko.General.Extensions.Jwt;

namespace Tasko.General.Models
{
    public class JwtValidationParameter
    {
        public string Issuer { get; set; }
        public string Audienece { get; set; }
        public string Key { get; set; }
        public SymmetricSecurityKey SymmetricSecurityKey { get { return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key)); } }

        public JwtValidationParameter()
        {

        }

        public JwtValidationParameter(IConfiguration configuration)
        {
            JwtValidationParameter parameter = configuration.GetJwtValidationParameter();

            Issuer = parameter.Issuer;
            Audienece = parameter.Audienece;
            Key = parameter.Key;
        }
        public required string Issuer { get; init; }
        public required string Audienece { get; init; }
        public required string Key { get; init; }
        public  SymmetricSecurityKey SymmetricSecurityKey { get { return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key)); } }
    }


}
