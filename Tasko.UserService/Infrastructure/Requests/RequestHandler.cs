using FluentValidation.Results;
using Tasko.Domains.Models.DTO.User;
using Tasko.General.Models.RequestResponses;
namespace Tasko.Service.Infrastructure.Requests;

internal static class RequestHandler
{
    internal static Func<IUserRepository, IMapper, CancellationToken, Task<IResult>> GetUsers()
    {
        return async (IUserRepository repository, IMapper mapper, CancellationToken cancellationToken) =>
        {
            var users = await repository.GetAsync(cancellationToken);

            return Results.Ok(new GetRequestResponse<IUser>(users));

        };
    }

    internal static Func<IUserRepository, ValidationParameter, CancellationToken, IMapper, UserCreate, IValidator<IUserCreate>, Task<IResult>> CreateUser()
    {
        return async (IUserRepository userRepository, ValidationParameter jwtValidationParameter, CancellationToken cancellationToken, IMapper mapper, UserCreate userCreate, IValidator<IUserCreate> validator) =>
        {
            var validationResult = validator.Validate(userCreate);

            if (!validationResult.IsValid)
            {
                var result = new BadRequestResponse<List<ValidationFailure>>(validationResult.Errors, "Validation error");
                return Results.BadRequest(result);
            }

            return Results.Ok();
        };
    }
}