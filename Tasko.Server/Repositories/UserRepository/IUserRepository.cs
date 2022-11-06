using Tasko.Server.Context.Data.Models;

namespace Tasko.Server.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
