namespace Tasko.Service.Infrastructure.Repositories
{
    public interface IUserRepository : IRepository<IUser>
    {
        Task<IEnumerable<IPermission>> GetUserPermissions(IUser user, CancellationToken cancellationToken = default);
        Task<IEnumerable<IPermission>> GetUserRolesPermissions(IUser user, CancellationToken cancellationToken = default);
        Task<IEnumerable<IRefreshToken>> GetRefreshTokensAsync(IUser id, CancellationToken cancellationToken = default);
        Task<IEnumerable<IRole>> GetUserRoles(IUser user, CancellationToken cancellationToken = default);
    }
}
