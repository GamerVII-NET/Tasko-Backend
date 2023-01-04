using System.Linq.Expressions;
using static System.Net.WebRequestMethods;

namespace Tasko.Service.Infrastructure.Repositories;

internal class RoleRepository : RoleRepositoryBase, IRoleRepository
{
    public RoleRepository(IMongoDatabase mongoDatabase, ValidationParameter validationParameter) : base(mongoDatabase, validationParameter)
    {
    }

    public Task<IRole> CreateAsync(Role model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IRole> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IRole>> FindManyAsync(Expression<Func<Role, bool>> expression, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IRole> FindOneAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IRole> FindOneAsync(Expression<Func<Role, bool>> expression, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<IRole>> GetAsync(CancellationToken cancellationToken = default)
    {
        var roles = await RolesCollection.FindAsync(_ => true);

        return await roles.ToListAsync();
    }

    public Task<IRole> UpdateAsync(Role model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}