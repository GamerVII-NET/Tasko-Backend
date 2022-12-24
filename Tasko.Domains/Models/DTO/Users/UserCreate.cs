namespace Tasko.Domains.Models.DTO.User
{
    public interface IUserCreate
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Patronymic { get; set; }
        string Email { get; set; }
        string Login { get; set; }
        string Password { get; set; }
    }

    public class UserCreate : IUserCreate
    {
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
    }
}
