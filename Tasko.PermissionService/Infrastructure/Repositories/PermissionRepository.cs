using System.Linq.Expressions;
using Tasko.Domains.Models.Structural;
using static System.Net.WebRequestMethods;

namespace Tasko.Service.Infrastructure.Repositories
{
    internal class PermissionRepository : PermissionRepositoryBase, IPermissionRepository
    {
        public PermissionRepository(IMongoDatabase mongoDatabase, ValidationParameter validationParameter) : base(mongoDatabase, validationParameter)
        {
        }

        public async Task<IPermission> CreateAsync(Permission permission, CancellationToken cancellationToken = default)
        {
            await PermissionCollection.InsertOneAsync(permission, cancellationToken: cancellationToken);

            permission.CreatedAt = DateTime.UtcNow;

            return await FindOneAsync(c => c.Name == permission.Name, cancellationToken);
        }

        public async Task<IPermission> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var deletedPernission = await PermissionCollection.FindOneAndDeleteAsync(c => c.Id == id, cancellationToken: cancellationToken);

            return deletedPernission;
        }

        public Task<IEnumerable<IPermission>> FindManyAsync(Expression<Func<Permission, bool>> expression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IPermission> FindOneAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var users = await PermissionCollection.FindAsync(u => u.Id == id, null, cancellationToken);
            return await users.SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<IPermission> FindOneAsync(Expression<Func<Permission, bool>> expression, CancellationToken cancellationToken = default)
        {
            var permissions = await PermissionCollection.FindAsync(expression, cancellationToken: cancellationToken);

            return await permissions.SingleOrDefaultAsync(cancellationToken);
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