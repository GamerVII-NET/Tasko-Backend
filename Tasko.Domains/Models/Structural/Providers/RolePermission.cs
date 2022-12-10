namespace Tasko.Domains.Models.Structural.Providers
{
    public interface IPermissionRole
    {
        Guid RoleId { get; set; }
        Guid PermissionId { get; set; }
    }

    public class RolePermission : IPermissionRole
    {
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
    }
}
