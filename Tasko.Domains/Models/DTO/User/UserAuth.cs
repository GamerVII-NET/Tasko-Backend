namespace Tasko.Domains.Models.DTO.User
{
    public interface IBaseUserAuth
    {
        string Login { get; set; }
        string Password { get; set; }
    }

    public class UserAuth : IBaseUserAuth
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
