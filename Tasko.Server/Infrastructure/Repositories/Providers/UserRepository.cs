using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Tasko.Domains.Models.DTO.Interfaces;
using Tasko.Domains.Models.Structural.Interfaces;
using Tasko.Domains.Models.Structural.Providers;
using Tasko.Server.Repositories.Abstracts;
using Tasko.Server.Repositories.Interfaces;

namespace Tasko.Server.Repositories.Providers
{
    public class UserRepository : UserRepositoryBase, IUserRepository
    {
        public UserRepository(IMongoDatabase databaseContext) : base(databaseContext) { }

        public async Task<IEnumerable<IUser>> GetUsersAsync() => await UserCollection.Find(_ => true).ToListAsync();

        public async Task<IUser> FindUserAsync(Guid id) => await UserCollection.Find(u => u.Id == id).SingleOrDefaultAsync();

        public async Task<IUser> FindUserAsync(string email)
        {
            var filter = Builders<IUser>.Filter.Eq("Email", email);
            return await UserCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task CreateUserAsync(IUser user) => await UserCollection.InsertOneAsync(user);
  
        public Task UpdateUserAsync(IUser user)
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
    }
}
