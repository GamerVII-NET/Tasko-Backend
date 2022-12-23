namespace Tasko.Domains.Models.DTO.User
{
    public interface IUserUpdate
    {
        #region Properties
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Patronymic { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string About { get; set; }
        #endregion
    }

    public class UserUpdate : IUserUpdate
    {
        #region Properties
        #endregion
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string About { get; set; }
    }
}
