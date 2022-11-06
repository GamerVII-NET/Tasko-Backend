using AutoMapper;
using Tasko.Domains.DTOs.User;
using Tasko.Server.Repositories.UserRepository;

namespace Tasko.Server.Services.API;

public class UserService
{
    internal static Func<IUserRepository, IMapper, Task<IResult>> GetUsersList()
    {
        return async (IUserRepository repository, IMapper mapper) =>
        {
            var users = await repository.GetAllUsersAsync();

            return Results.Ok(mapper.Map<IEnumerable<UserReadDto>>(users));
        };
    }
}
