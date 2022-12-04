using Tasko.Domains.Models.DTO.Interfaces;

namespace Tasko.Domains.Models.DTO.Providers
{
    public class UserRead : IUserRead
    {
        public UserRead(Guid globalGuid, string firstName, string lastName, string patronymic, 
                        string photo, string? about, DateTime lastOnline, bool isDeleted)
        {
            GlobalGuid = globalGuid;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            Photo = photo;
            About = about;
            LastOnline = lastOnline;
            IsDeleted = isDeleted;
        }

        public Guid GlobalGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Photo { get; set; }
        public string? About { get; set; }
        public DateTime LastOnline { get; set; }
        public bool IsDeleted { get; set; }
    }
}
