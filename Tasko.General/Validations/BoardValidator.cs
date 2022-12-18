using FluentValidation;
using Tasko.Domains.Models.DTO.Board;

namespace Tasko.General.Validations;

public class CreateBoardValidator : AbstractValidator<IBoardCreate>
{
    public CreateBoardValidator()
    {

        RuleFor(c => c.Name)
            .NotEmpty()
            .MinimumLength(4);

    }
}
public class UpdateBoardValidator : AbstractValidator<IBoardUpdate>
{
    public UpdateBoardValidator()
    {

        RuleFor(c => c.Name)
            .NotEmpty()
            .MinimumLength(4);

    }
}