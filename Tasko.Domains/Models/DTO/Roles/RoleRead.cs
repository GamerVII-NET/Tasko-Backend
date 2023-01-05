using Tasko.Domains.Models.Structural;

namespace Tasko.Domains.Models.DTO.Role;

public interface IRoleRead
{
    Guid Id { get; set; }
    string Name { get; set; }
    string Description { get; set; }

    IEnumerable<IPermission> Permissions { get; set; }
}

public class RoleRead : IRoleRead
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<IPermission> Permissions { get; set; }
}
