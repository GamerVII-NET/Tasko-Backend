using Tasko.Domains.Models.Structural.Interfaces;

namespace Tasko.Server.Repositories.Interfaces
{
    public interface IUserRepository : ITokenService
    {
        Task<IEnumerable<IUser>> GetUsersAsync();
        Task CreateUserAsync(IUser user);
        Task UpdateUserAsync(IUser user);
        Task DeleteUserAsync(Guid id);
        Task <IUser> FindUserAsync(Guid id);
        Task <IUser> FindUserAsync(string email);
    }
}
