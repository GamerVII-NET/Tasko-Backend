namespace Tasko.Domains.Models.Structural
{
    public interface IRolePermission
    {
        Guid RoleId { get; set; }
        Guid PermissionId { get; set; }
    }

    public class RolePermission : IRolePermission
    {
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
    }
}
