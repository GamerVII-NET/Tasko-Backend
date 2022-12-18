namespace Tasko.Domains.Models.DTO.Board;

public interface IBoardUpdate
{
    Guid Id { get; set; }
    string Avatar { get; set; }
    string Name { get; set; }
}

public class BoardUpdate : IBoardUpdate
{
    public Guid Id { get; set; }
    public string Avatar { get; set; }
    public string Name { get; set; }
}
