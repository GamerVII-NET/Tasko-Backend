using System.Linq.Expressions;

namespace Tasko.Service.Infrastructure.Repositories;

internal class RoleRepository : RoleRepositoryBase, IRoleRepository
{
    public RoleRepository(IMongoDatabase mongoDatabase, ValidationParameter validationParameter) :
    base(mongoDatabase, validationParameter)
    { }

    public async Task<IEnumerable<IRole>> GetAsync(CancellationToken cancellationToken = default)
    {
        var roles = await RolesCollection.FindAsync(r => true, cancellationToken: cancellationToken);
        return roles.ToEnumerable();
    }
    public async Task<IRole> FindOneAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var users = await RolesCollection.FindAsync(r => r.Id == id, cancellationToken: cancellationToken);
        return await users.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IRole> FindOneAsync(Expression<Func<Role, bool>> expression, CancellationToken cancellationToken = default)
    {
        var users = await RolesCollection.FindAsync(expression, cancellationToken: cancellationToken);
        return await users.SingleOrDefaultAsync(cancellationToken);
    }
    public async Task<IEnumerable<IRole>> FindManyAsync(Expression<Func<Role, bool>> expression, CancellationToken cancellationToken = default)
    {
        var users = await RolesCollection.FindAsync(expression, cancellationToken: cancellationToken);
        return users.ToEnumerable();
    }

    public async Task<IRole> CreateAsync(Role role, CancellationToken cancellationToken = default)
    {
        await RolesCollection.InsertOneAsync(role, cancellationToken: cancellationToken);
        return role;
    }

    public async Task<IRole> UpdateAsync(Role role, CancellationToken cancellationToken = default)
    {
        var foundedRole = await RolesCollection.FindOneAndReplaceAsync(r => r.Id == r.Id, role, cancellationToken: cancellationToken);
        return foundedRole;
    }
    public async Task<IRole> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var foundedRole = await RolesCollection.FindOneAndDeleteAsync(r => r.Id == id, cancellationToken: cancellationToken);
        return foundedRole;
    }
}