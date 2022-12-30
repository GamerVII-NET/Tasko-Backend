using MongoDB.Bson.Serialization.Attributes;

namespace Tasko.Domains.Models.Structural.Providers;

public interface IRefreshToken
{
    Guid Id { get; set; }
    Guid UserId { get; set; }
    string Token { get; set; }
    string CreatedByIp { get; set; }
    string RevokedByIp { get; set; }
    string ReasonRevoked { get; set; }
    bool IsExpired { get; }
    bool IsRevoked { get; }
    bool IsActive { get; }
    DateTime? RevokedAt { get; set; }
    DateTime IssuedAt { get; set; }
    DateTime ExpiresAt { get; set; }
}

public class RefreshToken : IRefreshToken
{
    [BsonId]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTime IssuedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string CreatedByIp { get; set; }
    public string RevokedByIp { get; set; }
    public string ReasonRevoked { get; set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsRevoked => RevokedAt != null;
    public bool IsActive => !IsRevoked && !IsExpired;
}