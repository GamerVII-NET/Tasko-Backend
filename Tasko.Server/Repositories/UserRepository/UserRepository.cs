using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users!.ToListAsync();
        }
    }
}
