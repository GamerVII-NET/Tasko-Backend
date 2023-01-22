namespace Tasko.Domains.Models.DTO.Permissions;

public interface IPermissionCreate
{
    string Name { get; set; }
    string DisplayName { get; set; }
    string Description { get; set; }
}
public class PermissionCreate : IPermissionCreate
{
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
}
