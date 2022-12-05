using MongoDB.Bson.Serialization.Attributes;

namespace Tasko.Domains.Models.Structural.Interfaces
{
    public interface IUser
    {
        Guid Id { get; set; }
        Guid RoleId { get; set; }
        string Email { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        IEnumerable<Guid> PhonesId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string? Patronymic { get; set; }
        string? Photo { get; set; }
        string? About { get; set; }
        DateTime LastOnline { get; set; }
        bool IsDeleted { get; set; }
    }
}
