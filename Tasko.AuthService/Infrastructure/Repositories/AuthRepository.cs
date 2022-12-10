using MongoDB.Driver;
using Tasko.Domains.Models.DTO.User;
using Tasko.Domains.Models.Structural.Providers;
using Tasko.General.Commands;
using Tasko.General.Models;

namespace Tasko.AuthService.Infrastructure.Repositories
{
    public interface IAuthRepository
    {
        Task<dynamic> AuthorizationAsync(IUserAuth userAuth, JwtValidationParameter jwtValidationParameter);
        Task<IUser> FindUserAsync(string login);
    }

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

    public class AuthRepository : AuthRepositoryBase, IAuthRepository
    {
        public AuthRepository(IMongoDatabase databaseContext) : base(databaseContext) {}

        public async Task<dynamic> AuthorizationAsync(IUserAuth userAuth, JwtValidationParameter jwtValidationParameter)
        {
            var user = await FindUserAsync(userAuth.Login);
            if (user == null) return Results.NotFound();
            var validatePassword = BCrypt.Net.BCrypt.Verify(userAuth.Password, user.Password);
            if (!validatePassword) return Results.Unauthorized();
            string token = Jwt.CreateToken(jwtValidationParameter, user);
            var userData = new
            {
                User = user,
                Token = token,
            };
            return userData;
        }

        public async Task<IUser> FindUserAsync(string login)
        {
            var filter = Filter.Eq("Login", login);
            return await UserCollection.Find(filter).FirstOrDefaultAsync();
        }
    }

}
