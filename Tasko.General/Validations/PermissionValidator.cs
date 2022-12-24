using Tasko.Domains.Models.DTO.Permissions;

namespace Tasko.General.Validations
{
    public class PermissionValidator : AbstractValidator<IPermission>
    {
        public PermissionValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                                .MinimumLength(4);

            RuleFor(c => c.DisplayName).NotEmpty()
                                       .MinimumLength(4);

        }
    }
    public class PermissionCreateValidator : AbstractValidator<IPermissionCreate>
    {
        public PermissionCreateValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                                .MinimumLength(4);

            RuleFor(c => c.DisplayName).NotEmpty()
                                       .MinimumLength(4);

        }
    }
    public class PermissionUpdateValidator : AbstractValidator<IPermissionUpdate>
    {
        public PermissionUpdateValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                                .MinimumLength(4);

            RuleFor(c => c.DisplayName).NotEmpty()
                                       .MinimumLength(4);

        }
    }
}
