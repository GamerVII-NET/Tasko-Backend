using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MongoDB.Driver;
using Tasko.Domains.Models.DTO.User;
using Tasko.Domains.Models.Structural.Providers;
using Tasko.General.Commands;
using Tasko.General.Models;
using Tasko.General.Models.RequestResponses;

namespace Tasko.AuthService.Infrastructure.Repositories
{
    #region Interfaces
    public interface IAuthRepository
    {
        Task<IResult> AuthorizationAsync(IUserAuth userAuth, JwtValidationParameter jwtValidationParameter, IMapper mapper, IValidator<IUserAuth> validator);
        Task<IUser> FindUserAsync(string login);
        Task<List<Role>> GetUserRoles(IUser user);
        Task<List<Permission>> GetUserRolesPermissions(IUser user);
        Task<List<Permission>> GetUserPermissions(IUser user);
    }
    #endregion

    #region Base classes
    public class AuthRepositoryBase
    {
        public AuthRepositoryBase(IMongoDatabase databaseContext)
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

    public class AuthRepository : AuthRepositoryBase, IAuthRepository
    {
        public AuthRepository(IMongoDatabase databaseContext) : base(databaseContext) { }

        public async Task<IResult> AuthorizationAsync(IUserAuth userAuth, JwtValidationParameter jwtValidationParameter, IMapper mapper, IValidator<IUserAuth> validator)
        {
            var validationResult = validator.Validate(userAuth);

            if (!validationResult.IsValid)
            {
                var result = new BadRequestResponse<List<ValidationFailure>>(validationResult.Errors, "Ошибка валидации данных");
                return Results.BadRequest(result);
            }

            var user = await FindUserAsync(userAuth.Login);
            if (user == null)
                return Results.Conflict(new BadRequestResponse<string>(
                "Пользователь с указанными данными не сущуствует!",
                "Пользователь с указанными данными не сущуствует!",
                StatusCodes.Status404NotFound));

            var validatePassword = BCrypt.Net.BCrypt.Verify(userAuth.Password, user.Password);
            if (!validatePassword)
            {
                var resposne = new List<ValidationFailure> { 
                    new ValidationFailure("Password", "Неверный логин или пароль") 
                };
                return Results.BadRequest(new BadRequestResponse<List<ValidationFailure>>(resposne, "Неверный логин или пароль", StatusCodes.Status401Unauthorized));

            }
            var userPermissions = await GetUserPermissions(user);
            var userRolesPermissions = await GetUserRolesPermissions(user);
            var permissions = userPermissions.Concat(userRolesPermissions).ToList();
            string token = Jwt.CreateToken(jwtValidationParameter, user, permissions);

            var response = new UserAuthRead
            {
                User = mapper.Map<IUser, UserRead>(user),
                Token = token,
            };

            return Results.Ok(new RequestResponse<UserAuthRead>(response, StatusCodes.Status200OK));
        }
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

        public async Task<IUser> FindUserAsync(string login)
        {
            var filter = Filter.Eq("Login", login);
            return await UserCollection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
