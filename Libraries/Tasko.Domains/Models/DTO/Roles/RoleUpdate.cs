namespace Tasko.Domains.Models.DTO.Role;

public interface IRoleUpdate
{
    Guid Id { get; set; }
    string Name { get; set; }
    string Description { get; set; }
}

public class RoleUpdate : IRoleUpdate
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}