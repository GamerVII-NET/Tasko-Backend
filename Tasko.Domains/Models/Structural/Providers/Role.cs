using MongoDB.Bson.Serialization.Attributes;

namespace Tasko.Domains.Models.Structural.Providers
{

    public interface IRole
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool IsDeleted { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        DateTime DeletedAt { get; set; }
        List<Guid> PermissionsGuids { get; set; }
    }

    public class Role : IRole
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public List<Guid> PermissionsGuids { get; set; }
    }
}
