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

        public async Task CreateUserAsync(IUser user) => await UserCollection.InsertOneAsync(user);

        public Task UpdateUserAsync(IUser user)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public string CreateToken(string key, string issuer, IUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var auidience = string.Concat(issuer);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new JwtSecurityToken(issuer, auidience, claims,
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }


    }
}
