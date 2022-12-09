namespace Tasko.Domains.Models.DTO.Role
{
    public interface IRoleCreate
    {
        #region Properties
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }

        List<Guid> PermissionGuids { get; set; }
        #endregion
    }

    public class RoleCreate : IRoleCreate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Guid> PermissionGuids { get; set; }
    }
}
