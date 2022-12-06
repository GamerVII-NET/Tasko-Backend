using Tasko.Domains.Models.Structural.Interfaces;
using Tasko.Domains.Models.Structural.Providers;

namespace Tasko.Server.Repositories.Interfaces
{
    public interface IUserRepository : ITokenService
    {
        Task<IEnumerable<IUser>> GetUsersAsync();
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid id);
        Task <IUser> FindUserAsync(Guid id);
        Task <IUser> FindUserAsync(string login);
        Task <IUser> FindUserAsync(string login, string password);
        bool VerifyUser(IConfiguration configuration, HttpContext context, IUser user);
    }
}
