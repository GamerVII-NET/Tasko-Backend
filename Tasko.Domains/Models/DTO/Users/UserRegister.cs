namespace Tasko.Domains.Models.DTO.User
{
    public interface IUserRegister
    {
        public UserRead User { get; set; }
        public string Token { get; set; }
    }

    public class UserRegister : IUserRegister
    {
        public UserRead User { get; set; }
        public string Token { get; set; }
    }
}
