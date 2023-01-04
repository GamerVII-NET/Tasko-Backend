using Tasko.Domains.Models.Structural;

namespace Tasko.MicroService.Infrastructure.Repositories
{
    public class AuthRepositoryBase
    {
        public AuthRepositoryBase(IMongoDatabase databaseContext)
        {
            UserFilter = Builders<User>.Filter;
            RefreshTokenFilter = Builders<RefreshToken>.Filter;
            RolesCollection = databaseContext.GetCollection<Role>("Roles");
            UserRolesCollection = databaseContext.GetCollection<UserRole>("UserRoles");
            RolePermissionsCollection = databaseContext.GetCollection<RolePermission>("RolePermissions");
            UserPermissionsCollection = databaseContext.GetCollection<UserPermission>("UserPermissions");
            PermissionCollection = databaseContext.GetCollection<Permission>("Permissions");
            RefreshTokensCollection = databaseContext.GetCollection<RefreshToken>("RefreshTokens");
            UserCollection = databaseContext.GetCollection<User>("Users");
        }

        internal IMongoCollection<User> UserCollection { get; set; }
        internal IMongoCollection<Role> RolesCollection { get; set; }
        internal IMongoCollection<Permission> PermissionCollection { get; set; }
        internal IMongoCollection<UserRole> UserRolesCollection { get; set; }
        internal IMongoCollection<UserPermission> UserPermissionsCollection { get; set; }
        internal IMongoCollection<RolePermission> RolePermissionsCollection { get; set; }
        internal IMongoCollection<RefreshToken> RefreshTokensCollection { get; set; }
        internal FilterDefinitionBuilder<User> UserFilter { get; }
        internal FilterDefinitionBuilder<RefreshToken> RefreshTokenFilter { get; }
    }
}