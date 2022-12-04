namespace Tasko.Domains.Models.Structural.Interfaces
{
    public interface IComment
    {
        Guid Id { get; set; }
        Guid CardId { get; set; }
        Guid UserId { get; set; }
        string Message { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
