using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Tasko.Domains.Models.Structural.Interfaces;
using Tasko.Domains.Models.Structural.Providers;
using Tasko.Server.Repositories.Abstracts;
using Tasko.Server.Repositories.Interfaces;

namespace Tasko.Server.Repositories.Providers
{
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

        public async Task DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public string CreateToken(string key, string issuer, string audience, IUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, $"{user.Id}"),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
            };
            var expiryDuration = new TimeSpan(0, 30, 0, 0);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.Now.Add(expiryDuration), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        #endregion
    }
}
