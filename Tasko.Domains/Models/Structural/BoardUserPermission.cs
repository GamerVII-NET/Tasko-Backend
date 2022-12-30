namespace Tasko.Domains.Models.Structural
{

    public interface IBoardUserPermission
    {
        Guid UserId { get; set; }
        Guid PermissionId { get; set; }
    }

    public class BoardUserPermission : IBoardUserPermission
    {
        public Guid UserId { get; set; }
        public Guid PermissionId { get; set; }
    }
}
