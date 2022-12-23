namespace Tasko.Domains.Models.DTO.Permissions
{
    public interface IPermissionRead
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }

    public class PermissionRead : IPermissionRead
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
