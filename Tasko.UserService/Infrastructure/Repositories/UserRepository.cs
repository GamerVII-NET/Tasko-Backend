namespace Tasko.UserService.Infrasructure.Repositories
{
    #region Interfaces
    public interface IUserRepository
    {
        Task<IEnumerable<IUser>> GetUsersAsync();
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid id);
        Task<IUser> FindUserAsync(Guid id);
        Task<IUser> FindUserAsync(string login);
        Task<IUser> FindUserAsync(string login, string password);
        Task<List<Role>> GetUserRoles(IUser user);
        Task<List<Permission>> GetUserRolesPermissions(IUser user);
        Task<List<Permission>> GetUserPermissions(IUser user);
    }
    #endregion

    #region Abstracts
    public class UserRepositoryBase
    {
        public UserRepositoryBase(IMongoDatabase databaseContext)
        {
            Filter = Builders<User>.Filter;
            RolesCollection = databaseContext.GetCollection<Role>("Roles");
            UserRolesCollection = databaseContext.GetCollection<UserRole>("UserRoles");
            RolePermissionsCollection = databaseContext.GetCollection<RolePermission>("RolePermissions");
            UserPermissionsCollection = databaseContext.GetCollection<UserPermission>("UserPermissions");
            PermissionCollection = databaseContext.GetCollection<Permission>("Permissions");
            UserCollection = databaseContext.GetCollection<User>("Users");
        }

        internal IMongoCollection<User> UserCollection { get; set; }
        internal IMongoCollection<Role> RolesCollection { get; set; }
        internal IMongoCollection<Permission> PermissionCollection { get; set; }
        internal IMongoCollection<UserRole> UserRolesCollection { get; set; }
        internal IMongoCollection<UserPermission> UserPermissionsCollection { get; set; }
        internal IMongoCollection<RolePermission> RolePermissionsCollection { get; set; }
        internal FilterDefinitionBuilder<User> Filter { get; }
    }

    #endregion

    #region Providers
    public class UserRepository : UserRepositoryBase, IUserRepository
    {
        #region Constructors
        public UserRepository(IMongoDatabase databaseContext) : base(databaseContext) { }
        #endregion

        #region Methods

        #region Find
        public async Task<IUser> FindUserAsync(Guid id) => await UserCollection.Find(u => u.Id == id).SingleOrDefaultAsync();
        public async Task<IUser> FindUserAsync(string login)
        {
            var filter = Filter.Eq("Login", login);
            return await UserCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<IUser> FindUserAsync(string login, string password)
        {
            var loginFilter = Filter.Eq(x => x.Login, login);
            var passwordFilter = Filter.Eq(x => x.Password, password);
            var combineFilters = Filter.And(loginFilter, passwordFilter);
            return await UserCollection.Find(combineFilters).FirstOrDefaultAsync();
        }
        #endregion

        public async Task<IEnumerable<IUser>> GetUsersAsync() => await UserCollection.Find(_ => true).ToListAsync();
        public async Task CreateUserAsync(User user) => await UserCollection.InsertOneAsync(user);
        public async Task UpdateUserAsync(User user)
        {
            var filter = Filter.Eq("Id", user.Id);
            await UserCollection.ReplaceOneAsync(filter, user);
        }
        public async Task DeleteUserAsync(Guid id) => await UserCollection.DeleteOneAsync(c => c.Id == id);
        public async Task<List<Role>> GetUserRoles(IUser user)
        {
            var userRolesIdFilter = Builders<UserRole>.Filter.Eq(d => d.UserId, user.Id);
            var userRoles = await UserRolesCollection.Find(userRolesIdFilter).ToListAsync();
            var rolesIdFilter = Builders<Role>.Filter.In(d => d.Id, userRoles.Select(c => c.RoleId));
            return await RolesCollection.Find(rolesIdFilter).ToListAsync();
        }
        public async Task<List<Permission>> GetUserRolesPermissions(IUser user)
        {
            var roles = await GetUserRoles(user);
            var rolePermissionsIdFilter = Builders<RolePermission>.Filter.In(d => d.RoleId, roles.Select(c => c.Id));
            var rolePermissions = await RolePermissionsCollection.Find(rolePermissionsIdFilter).ToListAsync();
            var permissionsIdFilter = Builders<Permission>.Filter.In(d => d.Id, rolePermissions.Select(c => c.PermissionId));
            return await PermissionCollection.Find(permissionsIdFilter).ToListAsync();

        }
        public async Task<List<Permission>> GetUserPermissions(IUser user)
        {
            var userPermissionsIdFilter = Builders<UserPermission>.Filter.Eq(d => d.UserId, user.Id);
            var userPermissions = await UserPermissionsCollection.Find(userPermissionsIdFilter).ToListAsync();
            var permissionsIdFilter = Builders<Permission>.Filter.In(d => d.Id, userPermissions.Select(c => c.PermissionId));
            return await PermissionCollection.Find(permissionsIdFilter).ToListAsync();
        }
        #endregion
    } 
    #endregion
}