using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Tasko.Server.Context.Data.Context;
using Tasko.Server.Context.Data.Models;

namespace Tasko.Server.Repositories.UserRepository
{
    

    public class UserRepository : IUserRepository
    {
        private readonly DataBaseContext _context;

        public UserRepository(DataBaseContext context)
        {
            _context = context;
        }

        public string CreateToken(string key, string issuer, User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.GlobalGuid.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Role, user.RoleNavigation.Name),
            };

            var auidience = string.Concat(issuer);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new JwtSecurityToken(issuer, auidience, claims,
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public async void CreateUserAsync(User user) => await _context.Users.AddAsync(user);

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users!.ToListAsync();
        }


        async Task<User> IUserRepository.CreateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.PasswordHash = "PasswordHash256";
            user.LastOnline = DateTime.Now;
            user.RoleNavigation = await _context.Roles.FirstOrDefaultAsync(c => c.Name == "User");

            var userModel = await _context.AddAsync(user);

            return userModel.Entity;
        }

        async Task IUserRepository.SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
