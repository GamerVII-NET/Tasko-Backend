using Tasko.Domains.Models.DTO.Interfaces;

namespace Tasko.Domains.Models.DTO.Providers
{
    public class UserCreate : IUserCreate
    {
        #region Properties
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public bool IsDeleted { get; set; } = false;
        #endregion
    }
}
