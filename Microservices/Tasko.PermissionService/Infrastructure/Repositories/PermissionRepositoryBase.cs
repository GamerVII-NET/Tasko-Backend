namespace Tasko.Service.Infrastructure.Repositories
{
    internal abstract class PermissionRepositoryBase
    {
        internal readonly FilterDefinitionBuilder<User> UserFilter;
        internal readonly ValidationParameter ValidationParameter;

        internal readonly IMongoCollection<User> UserCollection;
        internal readonly IMongoCollection<RefreshToken> RefreshTokensCollection;
        internal readonly IMongoCollection<Permission> PermissionCollection;
        internal readonly IMongoCollection<RolePermission> RolePermissionsCollection;
        internal readonly IMongoCollection<UserPermission> UserPermissionsCollection;
        internal readonly IMongoCollection<UserRole> UserRolesCollection;
        internal readonly IMongoCollection<Role> RolesCollection;

        internal PermissionRepositoryBase(IMongoDatabase mongoDatabase, ValidationParameter validationParameter)
        {
            ValidationParameter = validationParameter;
            UserFilter = Builders<User>.Filter;
            UserCollection = mongoDatabase.GetCollection<User>("Users");
            RefreshTokensCollection = mongoDatabase.GetCollection<RefreshToken>("Roles");
            PermissionCollection = mongoDatabase.GetCollection<Permission>("Permissions");
            RolePermissionsCollection = mongoDatabase.GetCollection<RolePermission>("RolePermissions");
            UserPermissionsCollection = mongoDatabase.GetCollection<UserPermission>("UserPermissions");
            UserRolesCollection = mongoDatabase.GetCollection<UserRole>("UserRoles");
            RolesCollection = mongoDatabase.GetCollection<Role>("Roles");
        }
    }
}