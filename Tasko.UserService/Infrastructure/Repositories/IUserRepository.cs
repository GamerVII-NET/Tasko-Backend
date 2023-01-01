namespace Tasko.UserService.Infrastructure.Repositories
{
    public interface IUserRepository : IRepository<IUser>
    {
        Task<IEnumerable<IPermission>> GetUserPermissions(IUser user, CancellationToken cancellationToken = default);
        Task<IEnumerable<IPermission>> GetUserRolesPermissions(IUser user, CancellationToken cancellationToken = default);
        Task<IEnumerable<IRefreshToken>> GetRefreshTokensAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<IRole>> GetUserRoles(IUser user, CancellationToken cancellationToken = default);
        UserFilter = Builders<User>.Filter;
    }
}
