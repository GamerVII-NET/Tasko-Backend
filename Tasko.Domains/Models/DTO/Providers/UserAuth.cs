using Tasko.Domains.Models.DTO.Interfaces;

namespace Tasko.Domains.Models.DTO.Providers
{
    public class UserAuth : IUserAuth
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
