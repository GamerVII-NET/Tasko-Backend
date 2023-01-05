namespace Tasko.Service.Infrastructure.Repositories;
public interface IRoleRepository : IRepository<IRole, Role>
{
    Task<IEnumerable<IPermission>> GetRolePermissions(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<IPermission>> GetRolePermissions(IRole role, CancellationToken cancellationToken);
}