using Tasko.Domains.Models.DTO.Interfaces;

namespace Tasko.Domains.Models.DTO.Providers
{
    public class UserRead : IUserRead
    {
        #region Properties
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Photo { get; set; }
        public string? About { get; set; }
        public DateTime LastOnline { get; set; }
        public bool? IsDeleted { get; set; }
        #endregion
    }
}
