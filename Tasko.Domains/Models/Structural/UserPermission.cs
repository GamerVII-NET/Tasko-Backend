namespace Tasko.Domains.Models.Structural
{

    public interface IUserPermission
    {
        Guid UserId { get; set; }
        Guid PermissionId { get; set; }
    }

    public class UserPermission : IUserPermission
    {
        public Guid UserId { get; set; }
        public Guid PermissionId { get; set; }
    }
}
