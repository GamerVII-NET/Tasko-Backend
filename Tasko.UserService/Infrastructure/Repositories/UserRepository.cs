using System.Linq.Expressions;
using Tasko.Service.Infrastructure.Repositories;

namespace Tasko.Service.Infrastructure.Repositories;

internal class UserRepository : UserRepositoryBase, IUserRepository
{
    public UserRepository(IMongoDatabase mongoDatabase, ValidationParameter validationParameter) : base(mongoDatabase, validationParameter)
    {
    }

    public async Task<IEnumerable<IUser>> GetAsync(CancellationToken cancellationToken = default)
    {
        var users = await UserCollection.FindAsync(_ => true, cancellationToken: cancellationToken);
        return await users.ToListAsync();
    }

    public Task<IUser> CreateAsync(IUser model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IUser> DeleteAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IUser>> FindManyAsync(Expression<Func<IUser, bool>> expression, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IUser> FindOneAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var users = await UserCollection.FindAsync(c => c.Id == id);

        return await users.SingleOrDefaultAsync();

    }

    public Task<IUser> FindOneAsync(Expression<Func<IUser, bool>> expression, CancellationToken cancellationToken = default)
    {

    }

    public Task<IEnumerable<IRefreshToken>> GetRefreshTokensAsync(IUser id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IPermission>> GetUserPermissions(IUser user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IRole>> GetUserRoles(IUser user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IPermission>> GetUserRolesPermissions(IUser user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IUser> UpdateAsync(IUser model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }






    //public async Task<IUser> FindOneAsync(Guid id, CancellationToken cancellationToken = default) =>
    //await (await UserCollection.FindAsync(u => u.Id == id)).SingleOrDefaultAsync(cancellationToken);
    //public async Task<IUser> FindOneAsync(Expression<Func<IUser, bool>> expression, CancellationToken cancellationToken = default)
    //{

    //    var test = await (await UserCollection.FindAsync(expression, cancellationToken: cancellationToken)).SingleOrDefaultAsync(cancellationToken);

    //    return test;
    //}
    //public async Task<IEnumerable<IUser>> FindManyAsync(Expression<Func<IUser, bool>> expression, CancellationToken cancellationToken = default) =>
    //await (await UserCollection.FindAsync(expression)).ToListAsync(cancellationToken);
    //public async Task<IUser> CreateAsync(IUser user, CancellationToken cancellationToken = default)
    //{
    //    await UserCollection.InsertOneAsync(user, null, cancellationToken);
    //    return await FindOneAsync(c => c.Login == user.Login, cancellationToken);
    //}
    //public async Task<IUser> UpdateAsync(IUser user, CancellationToken cancellationToken = default) =>
    //await UserCollection.FindOneAndReplaceAsync(c => c.Id == user.Id, user, cancellationToken: cancellationToken);
    //public async Task<IUser> DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
    //await UserCollection.FindOneAndDeleteAsync(c => c.Id == id, cancellationToken: cancellationToken);
    //public async Task<IEnumerable<IPermission>> GetUserPermissions(IUser user, CancellationToken cancellationToken = default)
    //{
    //    var roles = await GetUserRoles(user, cancellationToken);
    //    var rolePermissionsIdFilter = Builders<IRolePermission>.Filter.In(d => d.RoleId, roles.Select(c => c.Id));
    //    var rolePermissions = await (await RolePermissionsCollection.FindAsync(rolePermissionsIdFilter, null, cancellationToken))
    //                                                                .ToListAsync(cancellationToken);
    //    var permissionsIdFilter = Builders<IPermission>.Filter.In(d => d.Id, rolePermissions.Select(c => c.PermissionId));
    //    return await (await PermissionCollection.FindAsync(permissionsIdFilter, null, cancellationToken))
    //                                            .ToListAsync(cancellationToken);
    //}
    //public async Task<IEnumerable<IPermission>> GetUserRolesPermissions(IUser user, CancellationToken cancellationToken = default)
    //{
    //    var userPermissionsIdFilter = Builders<IUserPermission>.Filter.Eq(d => d.UserId, user.Id);
    //    var userPermissions = await (await UserPermissionsCollection.FindAsync(userPermissionsIdFilter, null, cancellationToken))
    //                                                                .ToListAsync(cancellationToken);
    //    var permissionsIdFilter = Builders<IPermission>.Filter.In(d => d.Id, userPermissions.Select(c => c.PermissionId));
    //    return await (await PermissionCollection.FindAsync(permissionsIdFilter, null, cancellationToken))
    //                                            .ToListAsync(cancellationToken);
    //}
    //public async Task<IEnumerable<IRefreshToken>> GetRefreshTokensAsync(Guid id, CancellationToken cancellationToken = default)
    //{
    //    var refreshTokenFilter = Builders<IRefreshToken>.Filter.Eq(d => d.UserId, id);
    //    return await (await RefreshTokensCollection.FindAsync(refreshTokenFilter, null, cancellationToken))
    //                                               .ToListAsync(cancellationToken);
    //}
    //public async Task<IEnumerable<IRole>> GetUserRoles(IUser user, CancellationToken cancellationToken = default)
    //{
    //    var userRolesIdFilter = Builders<IUserRole>.Filter.Eq(d => d.UserId, user.Id);
    //    var userRoles = await (await UserRolesCollection.FindAsync(userRolesIdFilter, null, cancellationToken))
    //                                                    .ToListAsync(cancellationToken);
    //    var rolesIdFilter = Builders<IRole>.Filter.In(d => d.Id, userRoles.Select(c => c.RoleId));
    //    return await (await RolesCollection.FindAsync(rolesIdFilter,null, cancellationToken))
    //                                       .ToListAsync(cancellationToken);
    //}
}