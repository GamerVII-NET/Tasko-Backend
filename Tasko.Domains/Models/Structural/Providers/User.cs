using MongoDB.Bson.Serialization.Attributes;
using Tasko.Domains.Models.Structural.Interfaces;

namespace Tasko.Domains.Models.Structural.Providers
{
    public class User : IUser
    {
        [BsonId]
        public Guid Id { get ; set ; } = Guid.NewGuid();
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get ; set ; }
        public IEnumerable<Guid> PhonesId { get ; set ; }
        public string FirstName { get ; set ; }
        public string LastName { get ; set ; }
        public string? Patronymic { get ; set ; }
        public string? Photo { get ; set ; }
        public string? About { get ; set ; }
        public DateTime LastOnline { get ; set ; }
        public bool IsDeleted { get ; set ; }
    }
}
