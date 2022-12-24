
namespace Tasko.General.Models
{
    public class JwtValidationParameter
    {
        public string Issuer { get; set; }
        public string Audienece { get; set; }
        public string Key { get; set; }
        public SymmetricSecurityKey SymmetricSecurityKey { get { return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key)); } }
    }
}
