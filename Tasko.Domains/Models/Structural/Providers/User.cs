using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Tasko.Domains.Models.Structural.Interfaces;

namespace Tasko.Domains.Models.Structural.Providers
{
    public class User : IUser
    {
        [BsonId]
        public Guid Id { get; set; } = Guid.NewGuid();
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Login")]
        public string Login { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
        [BsonElement("RoleId")]
        public Guid RoleId { get; set; }
        [BsonElement("PhonesId")]
        public IEnumerable<Guid> PhonesId { get; set; }
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [BsonElement("LastName")]
        public string LastName { get; set; }
        [BsonElement("Patronymic")]
        public string? Patronymic { get; set; }
        [BsonElement("Photo")]
        public string? Photo { get; set; }
        [BsonElement("About")]
        public string? About { get; set; }
        [BsonElement("LastOnline")]
        public DateTime LastOnline { get; set; }
        [BsonElement("IsDeleted")]
        public bool IsDeleted { get; set; }
    }
}
