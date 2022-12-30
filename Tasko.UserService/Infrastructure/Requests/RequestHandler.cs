namespace Tasko.Service.Infrastructure.Requests;

internal static class RequestHandler
{
    internal static Func<IUserRepository, IMapper, Task<IResult>> GetUsers()
    {
        return [Authorize] async (IUserRepository repository, IMapper mapper) =>
        {
            var users = await repository.GetUsersAsync();

            return Results.Ok(users);

        };
    }
}