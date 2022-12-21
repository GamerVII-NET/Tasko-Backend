namespace Tasko.Domains.Models.DTO.User
{
    public interface IUserAuth
    {
        string Login { get; set; }
        string Password { get; set; }
    }

    public class UserAuth : IUserAuth
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
