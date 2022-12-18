namespace Tasko.Domains.Models.DTO.Board;

public interface IBoardCreate
{
    string Avatar { get; set; }
    string Name { get; set; }
}

public class BoardCreate : IBoardCreate
{

    public string Avatar { get; set; }
    public string Name { get; set; }
}
