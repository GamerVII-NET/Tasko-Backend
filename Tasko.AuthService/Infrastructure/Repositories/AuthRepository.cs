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
        Task<List<IPermission>> GetUserPermissions(IUser user);
    }
    #endregion

    #region Base classes
    public class AuthRepositoryBase
    {
        public AuthRepositoryBase(IMongoDatabase databaseContext)
        {
            Filter = Builders<User>.Filter;
            UserCollection = databaseContext.GetCollection<User>("Users");
            PermissionCollection = databaseContext.GetCollection<IPermission>("Permissions");
        }

        internal IMongoCollection<User> UserCollection { get; set; }
        internal IMongoCollection<IPermission> PermissionCollection { get; set; }
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
            if (!validatePassword) return Results.Unauthorized();
            var userPermissions = await GetUserPermissions(user);
            string token = Jwt.CreateToken(jwtValidationParameter, user, userPermissions);

            var response = new UserAuthRead
            {
                User = mapper.Map<IUser, UserRead>(user),
                Token = token,
            };

            return Results.Ok(new RequestResponse<UserAuthRead>(response, StatusCodes.Status200OK));
        }
        public async Task<List<IPermission>> GetUserPermissions(IUser user)
        {
            var idsFilter = Builders<IPermission>.Filter.In(d => d.Id, user.PermissionsId);
            return await PermissionCollection.Find(idsFilter).ToListAsync();
        }
        public async Task<IUser> FindUserAsync(string login)
        {
            var filter = Filter.Eq("Login", login);
            return await UserCollection.Find(filter).FirstOrDefaultAsync();
        }
    }

}
