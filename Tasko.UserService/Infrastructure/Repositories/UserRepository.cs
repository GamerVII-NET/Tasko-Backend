using System.Linq.Expressions;
using Tasko.Jwt.Models;

namespace Tasko.Service.Infrastructure.Repositories;

internal class UserRepository : UserRepositoryBase, IUserRepository
{
    public UserRepository(IMongoDatabase mongoDatabase, ValidationParameter validationParameter) : base(mongoDatabase, validationParameter) { }
    public async Task<IEnumerable<IUser>> GetAsync(CancellationToken cancellationToken = default)
    {
        var users = await UserCollection.FindAsync(_ => true, null, cancellationToken);
        return await users.ToListAsync(cancellationToken);
    }
    public async Task<IUser> FindOneAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var users = await UserCollection.FindAsync(u => u.Id == id, null, cancellationToken);
        return await users.SingleOrDefaultAsync(cancellationToken);
    }
    public async Task<IUser> FindOneAsync(Expression<Func<User, bool>> expression, CancellationToken cancellationToken = default)
    {
        var users = await UserCollection.FindAsync(expression, cancellationToken: cancellationToken);
        return await users.SingleOrDefaultAsync(cancellationToken);
    }
    public async Task<IEnumerable<IUser>> FindManyAsync(Expression<Func<User, bool>> expression, CancellationToken cancellationToken = default)
    {
        var users = await UserCollection.FindAsync(expression, null, cancellationToken);
        return await users.ToListAsync(cancellationToken);
    }
    public async Task<IUser> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await UserCollection.InsertOneAsync(user, null, cancellationToken);
        return await FindOneAsync(c => c.Login == user.Login, cancellationToken);
    }
    public async Task<IUser> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        var updatedUser = await UserCollection.FindOneAndReplaceAsync(c => c.Id == user.Id, user, cancellationToken: cancellationToken);
        return updatedUser;
    }
    public async Task<IUser> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var deletedUser = await UserCollection.FindOneAndDeleteAsync(c => c.Id == id, cancellationToken: cancellationToken);
        return deletedUser;
    }
    public async Task<IEnumerable<IPermission>> GetUserPermissions(Guid userId, CancellationToken cancellationToken = default)
    {
        var roles = await GetUserRoles(userId, cancellationToken);
        var rolePermissionsIdFilter = Builders<RolePermission>.Filter.In(d => d.RoleId, roles.Select(c => c.Id));

        var rolePermissions = await (await RolePermissionsCollection.FindAsync(rolePermissionsIdFilter, null, cancellationToken))
                                                                    .ToListAsync(cancellationToken);

        var permissionsIdFilter = Builders<Permission>.Filter.In(d => d.Id, rolePermissions.Select(c => c.PermissionId));

        return await (await PermissionCollection.FindAsync(permissionsIdFilter, null, cancellationToken))
                                                .ToListAsync(cancellationToken);
    }
    public async Task<IEnumerable<IPermission>> GetUserRolesPermissions(Guid userId, CancellationToken cancellationToken = default)
    {
        var userPermissionsIdFilter = Builders<UserPermission>.Filter.Eq(d => d.UserId, userId);

        var userPermissions = await (await UserPermissionsCollection.FindAsync(userPermissionsIdFilter, null, cancellationToken))
                                                                    .ToListAsync(cancellationToken);

        var permissionsIdFilter = Builders<Permission>.Filter.In(d => d.Id, userPermissions.Select(c => c.PermissionId));

        return await (await PermissionCollection.FindAsync(permissionsIdFilter, null, cancellationToken))
                                                .ToListAsync(cancellationToken);
    }
    public async Task<IEnumerable<IRefreshToken>> GetRefreshTokensAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var refreshTokenFilter = Builders<RefreshToken>.Filter.Eq(d => d.UserId, userId);

        var tokens = await RefreshTokensCollection.FindAsync(refreshTokenFilter, null, cancellationToken);

        return await tokens.ToListAsync(cancellationToken);
    }
    public async Task<IEnumerable<IRole>> GetUserRoles(Guid userId, CancellationToken cancellationToken = default)
    {
        var userRolesIdFilter = Builders<UserRole>.Filter.Eq(d => d.UserId, userId);

        var userRoles = await (await UserRolesCollection.FindAsync(userRolesIdFilter, null, cancellationToken))
                                                        .ToListAsync(cancellationToken);

        var rolesIdFilter = Builders<Role>.Filter.In(d => d.Id, userRoles.Select(c => c.RoleId));

        return await (await RolesCollection.FindAsync(rolesIdFilter, null, cancellationToken))
                                           .ToListAsync(cancellationToken);
    }
}