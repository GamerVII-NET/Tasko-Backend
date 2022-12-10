using MongoDB.Bson.Serialization.Attributes;

namespace Tasko.Domains.Models.Structural.Providers
{

    public interface IPermission
    {
        Guid GlobalId { get; set; }
        string Name { get; set; }
        string DisplayName { get; set; }
        string Description { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }

    public class Permission : IPermission
    {
        [BsonId]
        public Guid GlobalId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
