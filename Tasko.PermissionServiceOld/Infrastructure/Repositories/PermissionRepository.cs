using Tasko.Domains.Models.Structural;
using IPermission = Tasko.Domains.Models.Structural.IPermission;

namespace Tasko.PermissionService.Infrastructure.Repositories
{
    public interface IPermissionRepository
    {
        Task<IPermission> FindPermissionAsync(Guid id);
        Task<IPermission> FindPermissionAsync(string name);
        Task<IEnumerable<IPermission>> GetPermissionsAsync();
        Task<IPermission> CreatePermissionAsync(Permission permission);
        Task<IPermission> UpdatePermissionAsync(Permission oldPermission, Permission newPermission);
        Task<DeleteResult> DeletePermissionAsync(Guid id);
    }
    public class PermissionRepositoryBase
    {
        public PermissionRepositoryBase(IMongoDatabase databaseContext)
        {
            Filter = Builders<Permission>.Filter;
            PermissionCollection = databaseContext.GetCollection<Permission>("Permissions");
        }

        internal IMongoCollection<Permission> PermissionCollection { get; set; }
        internal FilterDefinitionBuilder<Permission> Filter { get; }
    }
    public class PermissionRepository : PermissionRepositoryBase, IPermissionRepository
    {
        public PermissionRepository(IMongoDatabase databaseContext) : base(databaseContext) { }
        public async Task<IPermission> FindPermissionAsync(Guid id)
        {
            var filter = Filter.Eq(c => c.Id, id);
            return await PermissionCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<IPermission> FindPermissionAsync(string name)
        {
            var filter = Filter.Eq(c => c.Name, name);
            return await PermissionCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<IPermission> CreatePermissionAsync(Permission permission)
        {
            await PermissionCollection.InsertOneAsync(permission);
            return permission;
        }
        public async Task<IPermission> UpdatePermissionAsync(Permission oldPermission, Permission newPermission)
        {
            await PermissionCollection.ReplaceOneAsync(Filter.Eq(c => c.Id, oldPermission.Id), newPermission);
            return newPermission;
        }
        public async Task<DeleteResult> DeletePermissionAsync(Guid id) => 
               await PermissionCollection.DeleteOneAsync(Filter.Eq(c => c.Id, id));

        public async Task<IEnumerable<IPermission>> GetPermissionsAsync() =>
               await PermissionCollection.Find(_ => true).ToListAsync();
        
    }
}
