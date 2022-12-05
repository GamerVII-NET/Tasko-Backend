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
            var filter = Builders<User>.Filter.Eq("Login", login);
            var sd = await UserCollection.Find(filter).ToListAsync();
            return sd.FirstOrDefault();
        }

        public async Task<IUser> FindUserAsync(string login, string password)
        {
            var loginFilter = Builders<User>.Filter.Eq(x => x.Login, login);
            var passwordFilter = Builders<User>.Filter.Eq(x => x.Password, password);
            var combineFilters = Builders<User>.Filter.And(loginFilter, passwordFilter);
            var sd = await UserCollection.Find(combineFilters).FirstOrDefaultAsync();
            return sd;
        }
        #endregion

        public async Task<IEnumerable<IUser>> GetUsersAsync() => await UserCollection.Find(_ => true).ToListAsync();

        public async Task CreateUserAsync(User user) => await UserCollection.InsertOneAsync(user);

        public Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
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
