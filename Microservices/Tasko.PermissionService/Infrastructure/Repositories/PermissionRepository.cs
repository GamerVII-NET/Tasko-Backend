using System.Linq.Expressions;
using static System.Net.WebRequestMethods;

namespace Tasko.Service.Infrastructure.Repositories
{
    internal class PermissionRepository : PermissionRepositoryBase, IPermissionService
    {
        public PermissionRepository(IMongoDatabase mongoDatabase, ValidationParameter validationParameter) : base(mongoDatabase, validationParameter)
        {
        }

        public Task<IPermission> CreateAsync(Permission model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPermission> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IPermission>> FindManyAsync(Expression<Func<Permission, bool>> expression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPermission> FindOneAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPermission> FindOneAsync(Expression<Func<Permission, bool>> expression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IPermission>> GetAsync(CancellationToken cancellationToken = default)
        {
            var permissions = await PermissionCollection.FindAsync(_ => true);

            return await permissions.ToListAsync();
        }

        public Task<IPermission> UpdateAsync(Permission model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}