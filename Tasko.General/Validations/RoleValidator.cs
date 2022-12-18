using FluentValidation;
using Tasko.Domains.Models.DTO.Role;
using Tasko.Domains.Models.DTO.User;

namespace Tasko.General.Validations;

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