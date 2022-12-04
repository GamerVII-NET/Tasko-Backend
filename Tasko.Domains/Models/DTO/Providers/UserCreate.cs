using Tasko.Domains.Models.DTO.Interfaces;

namespace Tasko.Domains.Models.DTO.Providers
{
    public class UserCreate : IUserCreate
    {
        #region Variables
        private string _password = string.Empty;
        #endregion

        #region Properties
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password
        {
            get { return _password; }
            set { _password = BCrypt.Net.BCrypt.HashPassword(value); }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public bool IsDeleted { get; set; } = false;
        #endregion
    }
}
