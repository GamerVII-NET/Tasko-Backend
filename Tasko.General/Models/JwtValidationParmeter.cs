
namespace Tasko.General.Models
{
    public class JwtValidationParameter
    {
        public required string Issuer { get; init; }
        public required string Audienece { get; init; }
        public required string Key { get; init; }
        public  SymmetricSecurityKey SymmetricSecurityKey { get { return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key)); } }
    }
}
