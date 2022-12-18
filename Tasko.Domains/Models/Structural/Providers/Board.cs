namespace Tasko.Domains.Models.Structural.Providers;

public interface IBoard
{
    Guid Id { get; set; }
    Guid AuthorId { get; set; }
    string Avatar { get; set; }
    string Name { get; set; }
    DateTime CreatedAt { get; set; }
}

public class Board : IBoard
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string Avatar { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}