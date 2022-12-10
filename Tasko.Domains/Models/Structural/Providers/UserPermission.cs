namespace Tasko.Domains.Models.Structural.Providers
{

    public interface IPermissionUsers
    {
        Guid UserId { get; set; }
        Guid PermissionId { get; set; }
    }

    public class UserPermission : IPermissionUsers
    {
        public Guid UserId { get; set; }
        public Guid PermissionId { get; set; }
    }
}
