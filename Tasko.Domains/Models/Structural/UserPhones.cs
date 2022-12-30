using MongoDB.Bson.Serialization.Attributes;

namespace Tasko.Domains.Models.Structural
{
    public interface IUserPhone
    {
        Guid Id { get; set; }
        Guid? CountryId { get; set; }
        string Number { get; set; }
        string FullNumber { get; set; }
    }
    public class UserPhone : IUserPhone
    {
        [BsonId]
        public Guid Id { get; set; }
        public Guid? CountryId { get; set; }
        public string Number { get; set; }
        public string FullNumber { get; set; }
    }
}
