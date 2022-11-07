using Tasko.Server.Context.Data.Models;

namespace Tasko.Server.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> CreateUserAsync(User user);

        Task SaveChangesAsync();

        string CreateToken(string key, string issuer, User user);
    }
}
