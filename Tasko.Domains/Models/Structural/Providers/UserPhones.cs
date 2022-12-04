using MongoDB.Bson.Serialization.Attributes;
using Tasko.Domains.Models.Structural.Interfaces;

namespace Tasko.Domains.Models.Structural.Providers
{
    public class UserPhone : IUserPhone
    {
        [BsonId]
        public Guid Id { get; set; }
        public Guid? CountryId { get; set; }
        public string Number { get; set; }
        public string FullNumber { get; set; }
    }
}
