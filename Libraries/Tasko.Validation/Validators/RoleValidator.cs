using Tasko.Domains.Models.DTO.Role;

namespace Tasko.Validation.Validators;

public class RoleCreateValidator : AbstractValidator<IRoleCreate>
{
    public RoleCreateValidator()
    {

        RuleFor(c => c.Name)
            .NotEmpty()
            .MinimumLength(4);

        RuleFor(c => c.Description)
            .NotEmpty()
            .MinimumLength(1);

    }
}

public class RoleUpdateValidator : AbstractValidator<IRoleUpdate>
{
    public RoleUpdateValidator()
    {

        RuleFor(c => c.Name)
            .NotEmpty()
            .MinimumLength(4);

        RuleFor(c => c.Description)
            .NotEmpty()
            .MinimumLength(1);

    }
}