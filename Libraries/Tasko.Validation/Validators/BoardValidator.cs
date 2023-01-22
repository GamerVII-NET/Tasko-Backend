using Tasko.Domains.Models.DTO.Board;

namespace Tasko.Validation.Validators;

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