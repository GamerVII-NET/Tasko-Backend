using FluentValidation.Results;
using Tasko.Domains.Models.DTO.Role;
using Tasko.Domains.Models.RequestResponse;

namespace Tasko.Service.Infrastructure.RequestHandlers;

internal static class RequestHandler
{
    internal static Func<IRoleRepository, ValidationParameter, IMapper, CancellationToken, Task<IResult>> GetRoles()
    {
        return [Authorize] async (IRoleRepository roleRepository, ValidationParameter jwtValidationParameter, IMapper mapper, CancellationToken cancellationToken) =>
        {

            var roles = await roleRepository.GetAsync(cancellationToken);
            //var roles = mapper.Map<IEnumerable<IRoleRead>>();

            //return Results.Ok(new GetRequestResponse<IUserRead>(roles));
            return Results.Ok();
        };
    }


    internal static Func<IRoleRepository, IMapper, RoleCreate, IValidator<IRoleCreate>, CancellationToken, Task<IResult>> CreateUser(ValidationParameter validationParameter)
    {
        return [AllowAnonymous] async (IRoleRepository roleRepository, IMapper mapper, RoleCreate roleCreate, IValidator<IRoleCreate> validator, CancellationToken cancellationToken) =>
        {
            var validationResult = validator.Validate(roleCreate);

            if (!validationResult.IsValid)
            {
                var result = new BadRequestResponse<List<ValidationFailure>>(validationResult.Errors, "Validation error");
                return Results.BadRequest(result);
            }

            return Results.Ok();
        };
    }
}