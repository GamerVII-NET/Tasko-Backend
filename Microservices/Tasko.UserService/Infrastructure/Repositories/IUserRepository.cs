namespace Tasko.Service.Infrastructure.Repositories;
public interface IUserRepository : IRepository<IUser, User>
{
    Task<IEnumerable<IPermission>> GetUserPermissions(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<IPermission>> GetUserRolesPermissions(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<IRefreshToken>> GetRefreshTokensAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<IRole>> GetUserRoles(Guid userId, CancellationToken cancellationToken = default);
}