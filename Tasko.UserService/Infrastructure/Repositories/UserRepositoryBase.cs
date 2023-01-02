namespace Tasko.Service.Infrastructure.Repositories;
internal abstract class UserRepositoryBase
{
    internal readonly ValidationParameter ValidationParameter;
    internal readonly FilterDefinitionBuilder<IUser> UserFilter;
    internal readonly IMongoCollection<User> UserCollection;
    internal readonly IMongoCollection<IRefreshToken> RefreshTokensCollection;
    internal readonly IMongoCollection<IPermission> PermissionCollection;
    internal readonly IMongoCollection<IRolePermission> RolePermissionsCollection;
    internal readonly IMongoCollection<IUserPermission> UserPermissionsCollection;
    internal readonly IMongoCollection<IUserRole> UserRolesCollection;
    internal readonly IMongoCollection<IRole> RolesCollection;
    internal UserRepositoryBase(IMongoDatabase mongoDatabase, ValidationParameter validationParameter)
    {
        ValidationParameter = validationParameter;
        UserFilter = Builders<IUser>.Filter;
        UserCollection = mongoDatabase.GetCollection<User>("Users");
        RefreshTokensCollection = mongoDatabase.GetCollection<IRefreshToken>("RefreshTokens");
        PermissionCollection = mongoDatabase.GetCollection<IPermission>("Permissions");
        RolePermissionsCollection = mongoDatabase.GetCollection<IRolePermission>("RolePermissions");
        UserPermissionsCollection = mongoDatabase.GetCollection<IUserPermission>("UserPermissions");
        UserRolesCollection = mongoDatabase.GetCollection<IUserRole>("UserRoles");
        RolesCollection = mongoDatabase.GetCollection<IRole>("Roles");
    }
}