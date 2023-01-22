namespace Tasko.Domains.Models.Structural;
public interface IUserBoardRole
{
    Guid BoardId { get; set; }
    Guid PermissionId { get; set; }
}

public class BoardUserRole : IUserBoardRole
{
    public Guid BoardId { get; set; }
    public Guid PermissionId { get; set; }
}