namespace Tasko.Domains.Models.Structural.Providers;

public interface IBoardRead
{
    Guid Id { get; set; }
    Guid AuthorId { get; set; }
    string Avatar { get; set; }
    string Names { get; set; }
    DateTime CreatedAt { get; set; }
}

public class BoardRead : IBoardRead
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string Avatar { get; set; }
    public string Names { get; set; }
    public DateTime CreatedAt { get; set; }
}