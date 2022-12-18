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
        Task<IResult> AuthorizationAsync(IBaseUserAuth userAuth, JwtValidationParameter jwtValidationParameter, IMapper mapper, IValidator<IBaseUserAuth> validator);
        Task<IUser> FindUserAsync(string login);
    }
    #endregion

    #region Base classes
    public class AuthRepositoryBase
    {
        public AuthRepositoryBase(IMongoDatabase databaseContext)
        {
            Filter = Builders<User>.Filter;
            UserCollection = databaseContext.GetCollection<User>("Users");
        }

        internal IMongoCollection<User> UserCollection { get; set; }

        internal FilterDefinitionBuilder<User> Filter { get; }
    }
    #endregion

    public class AuthRepository : AuthRepositoryBase, IAuthRepository
    {
        public AuthRepository(IMongoDatabase databaseContext) : base(databaseContext) { }

        public async Task<IResult> AuthorizationAsync(IBaseUserAuth userAuth, JwtValidationParameter jwtValidationParameter, IMapper mapper, IValidator<IBaseUserAuth> validator)
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
            string token = Jwt.CreateToken(jwtValidationParameter, user);

            var response = new UserAuthRead
            {
                User = mapper.Map<IUser, UserRead>(user),
                Token = token,
            };

            return Results.Ok(new RequestResponse<UserAuthRead>(response, StatusCodes.Status200OK));
        }

        public async Task<IUser> FindUserAsync(string login)
        {
            var filter = Filter.Eq("Login", login);
            return await UserCollection.Find(filter).FirstOrDefaultAsync();
        }
    }

}
