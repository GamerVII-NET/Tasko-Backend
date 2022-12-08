namespace Tasko.Domains.Models.DTO.User
{
    public interface IUserCreate
    {
        #region Properties
        string FirstName { get; set; }
        string LastName { get; set; }
        string Patronymic { get; set; }
        string Email { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        #endregion
    }

    public class UserCreate : IUserCreate
    {
        #region Properties
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        #endregion
    }
}
