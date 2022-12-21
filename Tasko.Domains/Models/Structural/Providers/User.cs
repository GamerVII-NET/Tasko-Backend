using MongoDB.Bson.Serialization.Attributes;

namespace Tasko.Domains.Models.Structural.Providers
{
    public interface IUser
    {
        Guid Id { get; set; }
        string Email { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        IEnumerable<Guid> PermissionsId { get; set; }
        IEnumerable<Guid> PhonesId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string? Patronymic { get; set; }
        string? Photo { get; set; }
        string? About { get; set; }
        DateTime LastOnline { get; set; }
        bool IsDeleted { get; set; }
    }

    public class User : IUser
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public IEnumerable<Guid> PermissionsId { get; set; }
        public IEnumerable<Guid> PhonesId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Patronymic { get; set; }
        public string? Photo { get; set; }
        public string? About { get; set; }
        public DateTime LastOnline { get; set; }
        public bool IsDeleted { get; set; }
    }
}
