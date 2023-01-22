using Tasko.Domains.Models.DTO.Role;

namespace Tasko.Validation.Validators;

public class CreateRoleValidator : AbstractValidator<IRoleCreate>
{
    public CreateRoleValidator()
    {

        RuleFor(c => c.Name)
            .NotEmpty()
            .MinimumLength(4);

        RuleFor(c => c.Description)
            .NotEmpty()
            .MinimumLength(1);

    }
}