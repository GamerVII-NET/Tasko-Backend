using MongoDB.Bson.Serialization.Attributes;
using Tasko.Domains.Models.Structural.Interfaces;

namespace Tasko.Domains.Models.Structural.Providers
{
    public class Role : IRole
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
