using MongoDB.Bson;
using System.Linq.Expressions;
using Tasko.Domains.Models.Structural;
using static System.Net.WebRequestMethods;

namespace Tasko.Service.Infrastructure.Repositories;

internal class RoleRepository : RoleRepositoryBase, IRoleRepository
{
    public RoleRepository(IMongoDatabase mongoDatabase, ValidationParameter validationParameter) : base(mongoDatabase, validationParameter)
    {
    }

    public async Task<IRole> CreateAsync(Role role, CancellationToken cancellationToken = default)
    {
        await RolesCollection.InsertOneAsync(role, cancellationToken: cancellationToken);
        return await FindOneAsync(c => c.Name == role.Name, cancellationToken);
    }

    public Task<IRole> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IRole>> FindManyAsync(Expression<Func<Role, bool>> expression, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IRole> FindOneAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var users = await RolesCollection.FindAsync(u => u.Id == id, null, cancellationToken);
        return await users.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IRole> FindOneAsync(Expression<Func<Role, bool>> expression, CancellationToken cancellationToken = default)
    {
        var permissions = await RolesCollection.FindAsync(expression, cancellationToken: cancellationToken);

        return await permissions.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<IRole>> GetAsync(CancellationToken cancellationToken = default)
    {
        var roles = await RolesCollection.FindAsync(_ => true);

        return await roles.ToListAsync();
    }

    public async Task<IEnumerable<IPermission>> GetRolePermissions(IRole role, CancellationToken cancellationToken)
    {
        if (role.PermissionGuids.Count == 0)
        {
            return Enumerable.Empty<IPermission>();
        }
        var permissionsFilter = Builders<Permission>.Filter.In(d => d.Id, role.PermissionGuids);

        return await PermissionCollection.Find(permissionsFilter).ToListAsync(cancellationToken);
    }

    public Task<IEnumerable<IPermission>> GetRolePermissions(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IRole> UpdateAsync(Role model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}