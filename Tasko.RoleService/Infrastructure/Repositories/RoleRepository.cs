using MongoDB.Driver;
using System.Runtime.InteropServices;
using Tasko.Domains.Models.Structural.Providers;

namespace Tasko.RoleService.Infrasructure.Repositories;

#region Interfaces
#region User Roles
public interface IUserRoleRepository
{
    /// <summary>
    /// Get user roles
    /// </summary>
    /// <param name="user">Database user</param>
    /// <returns>Enumeration of user roles</returns>
    Task<IEnumerable<IRole>> GetUserRolesAsync(User user);

    /// <summary>
    /// Add roles to a user
    /// </summary>
    /// <param name="user">Database user</param>
    /// <param name="roles">Database roles</param>
    /// <returns>Enumeration of user roles</returns>
    Task<IEnumerable<IRole>> AddUserRolesAsync(User user, IEnumerable<Role> roles);

    /// <summary>
    /// Add permissions to a user
    /// </summary>
    /// <param name="user">Database user</param>
    /// <param name="permissions">Database permissions</param>
    /// <returns>Enumeration of user permissions</returns>
    Task<IEnumerable<Permission>> AddUserPermissionsAsync(User user, IEnumerable<Permission> permissions);

    /// <summary>
    /// Removing user roles
    /// </summary>
    /// <param name="user">Database user</param>
    /// <param name="role">Database roles</param>
    /// <returns>Enumeration of user roles</returns>
    Task<IEnumerable<Role>> RemoveUserRolesAsync(User user, IEnumerable<Role> role);

    /// <summary>
    /// Removing user permissions
    /// </summary>
    /// <param name="user">Database roles</param>
    /// <param name="permissions">Database permissions</param>
    /// <returns>Enumeration of user permissions</returns>
    Task<IEnumerable<Permission>> RemoveUserPermissionsAsync(User user, IEnumerable<Permission> permissions);

}
#endregion
#region Board Roles

public interface IBoardUserRoleRepository
{
    /// <summary>
    /// Get board user roles
    /// </summary>
    /// <param name="user">Database user</param>
    /// <returns>Enumeration of board user roles</returns>
    Task<IEnumerable<IRole>> GetBoardUserRolesAsync(BoardUser user);

    /// <summary>
    /// Add roles to a board user
    /// </summary>
    /// <param name="user">Database user</param>
    /// <param name="roles">Database roles</param>
    /// <returns>Enumeration of board user roles</returns>
    Task<IEnumerable<IRole>> AddBoardUserRolesAsync(BoardUser user, IEnumerable<Role> roles);

    /// <summary>
    /// Add permissions to a board user
    /// </summary>
    /// <param name="user">Database user</param>
    /// <param name="permissions">Database permissions</param>
    /// <returns>Enumeration of board user permissions</returns>
    Task<IEnumerable<Permission>> AddBoardUserPermissionsAsync(BoardUser user, IEnumerable<Permission> permissions);

    /// <summary>
    /// Removing board user roles
    /// </summary>
    /// <param name="user">Database user</param>
    /// <param name="role">Database roles</param>
    /// <returns>Enumeration of board user roles</returns>
    Task<IEnumerable<IRole>> RemoveBoardUserRolesAsync(BoardUser user, IEnumerable<Role> role);


    /// <summary>
    /// Removing board user permissions
    /// </summary>
    /// <param name="user">Database roles</param>
    /// <param name="permissions">Database permissions</param>
    /// <returns>Enumeration of board user permissions</returns>
    Task<IEnumerable<Permission>> RemoveBoardUserPermissionsAsync(BoardUser user, IEnumerable<Permission> permissions);
}
#endregion
#region Role permissions

public interface IRolePermissionRepository
{
    /// <summary>
    /// Adding role permissions
    /// </summary>
    /// <param name="role">Database role</param>
    /// <param name="permissions">Database permissions</param>
    /// <returns>Enumeration of role permissions</returns>
    Task<IEnumerable<Permission>> AddRolePermissionsAsync(Role role, IEnumerable<Permission> permissions);


    /// <summary>
    /// Adding role permissions
    /// </summary>
    /// <param name="role">Database role</param>
    /// <param name="permissions">Database permissions</param>
    /// <returns>Enumeration of role permissions</returns>
    Task<IEnumerable<Permission>> RemoveRolePermissionsAsync(Role role, IEnumerable<Permission> permissions);
}
#endregion
#region Role Repository Interface
public interface IRoleRepository
{
    /// <summary>
    /// Getting a list of roles
    /// </summary>
    /// <returns>Enumeration of roles</returns>
    Task<IEnumerable<IRole>> GetRolesAsync();

    /// <summary>
    /// Getting a role
    /// </summary>
    /// <param name="id">Role guid</param>
    /// <returns>Role</returns>
    Task<IRole> FindRoleAsync(Guid id);

    /// <summary>
    /// Getting a role
    /// </summary>
    /// <param name="id">Enumeration role guids</param>
    /// <returns>Enumeration roles</returns>
    Task<IEnumerable<IRole>> FindRolesAsync(IEnumerable<Guid> ids);

    /// <summary>
    /// Create role
    /// </summary>
    /// <param name="role">Database role</param>
    /// <param name="permissions">Database permissions</param>
    /// <returns>Created role</returns>
    Task<IRole> CreateRole(Role role, [Optional] IEnumerable<Permission> permissions);

    /// <summary>
    /// Update role
    /// </summary>
    /// <param name="role">Database role</param>
    /// <param name="permissions">Database permissions</param>
    /// <returns>Updated role</returns>
    Task<IRole> UpdateRole(Role role, IEnumerable<Permission> permissions);

    /// <summary>
    /// Delete role
    /// </summary>
    /// <param name="role">Database role</param>
    Task DeleteRoleAsync(Role role);

} 
#endregion
#endregion

#region Abstracts
public class RoleRepositoryBase
{
    public RoleRepositoryBase(IMongoDatabase databaseContext)
    {
        Filter = Builders<Role>.Filter;
        RolesCollection = databaseContext.GetCollection<Role>("Roles");
    }

    internal IMongoCollection<Role> RolesCollection { get; set; }

    internal FilterDefinitionBuilder<Role> Filter { get; }
}

#endregion

public class RoleRepository : RoleRepositoryBase, IRoleRepository, IRolePermissionRepository, IBoardUserRoleRepository, IUserRoleRepository
{
    public RoleRepository(IMongoDatabase databaseContext) : base(databaseContext)
    {
    }

    public Task<IEnumerable<Permission>> AddBoardUserPermissionsAsync(BoardUser user, IEnumerable<Permission> permissions)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IRole>> AddBoardUserRolesAsync(BoardUser user, IEnumerable<Role> roles)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Permission>> AddRolePermissionsAsync(Role role, IEnumerable<Permission> permissions)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Permission>> AddUserPermissionsAsync(User user, IEnumerable<Permission> permissions)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IRole>> AddUserRolesAsync(User user, IEnumerable<Role> roles)
    {
        throw new NotImplementedException();
    }

    public async Task<IRole> CreateRole(Role role, [Optional] IEnumerable<Permission> permissions)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRoleAsync(Role role)
    {
        throw new NotImplementedException();
    }

    public Task<IRole> FindRoleAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IRole>> FindRolesAsync(IEnumerable<Guid> ids)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IRole>> GetBoardUserRolesAsync(BoardUser user)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<IRole>> GetRolesAsync() => await RolesCollection.Find(_ => true).ToListAsync();

    public Task<IEnumerable<IRole>> GetUserRolesAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Permission>> RemoveBoardUserPermissionsAsync(BoardUser user, IEnumerable<Permission> permissions)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IRole>> RemoveBoardUserRolesAsync(BoardUser user, IEnumerable<Role> role)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Permission>> RemoveRolePermissionsAsync(Role role, IEnumerable<Permission> permissions)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Permission>> RemoveUserPermissionsAsync(User user, IEnumerable<Permission> permissions)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Role>> RemoveUserRolesAsync(User user, IEnumerable<Role> role)
    {
        throw new NotImplementedException();
    }

    public Task<IRole> UpdateRole(Role role, IEnumerable<Permission> permissions)
    {
        throw new NotImplementedException();
    }
}
