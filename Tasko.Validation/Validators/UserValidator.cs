using Tasko.Domains.Models.DTO.User;

namespace Tasko.Validation.Validators;

public class AuthUserValidator : AbstractValidator<IUserAuth>
{
    public AuthUserValidator()
    {

        RuleFor(c => c.Login)
            .NotEmpty()
            .MinimumLength(4);

        RuleFor(c => c.Password)
            .NotEmpty()
            .MinimumLength(6);

    }
}
public class CreateUserValidator : AbstractValidator<IUserCreate>
{
    public CreateUserValidator()
    {

        RuleFor(c => c.Login)
            .NotEmpty()
            .MinimumLength(4);

        RuleFor(c => c.Password)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(c => c.FirstName)
            .NotEmpty()
            .MinimumLength(1);

        RuleFor(c => c.Patronymic)
            .NotEmpty()
            .MinimumLength(1);

        RuleFor(c => c.LastName)
            .NotEmpty()
            .MinimumLength(1);

        RuleFor(c => c.Email)
            .NotEmpty()
            .MinimumLength(6);

    }
}
public class UpdateUserValidator : AbstractValidator<IUserUpdate>
{
    public UpdateUserValidator()
    {

        RuleFor(c => c.Id)
            .NotEmpty();

        RuleFor(c => c.Password)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(c => c.FirstName)
            .NotEmpty()
            .MinimumLength(1);

        RuleFor(c => c.Patronymic)
            .NotEmpty()
            .MinimumLength(1);

        RuleFor(c => c.LastName)
            .NotEmpty()
            .MinimumLength(1);

        RuleFor(c => c.Email)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(c => c.About)
            .MaximumLength(255);

    }
}