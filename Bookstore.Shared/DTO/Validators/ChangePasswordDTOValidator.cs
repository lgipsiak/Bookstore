using FluentValidation;

namespace Bookstore.Shared.DTO.Validators
{
    public class ChangePasswordDTOValidator : AbstractValidator<ChangePasswordDTO>
    {
        public ChangePasswordDTOValidator()
        {
            RuleFor(x => x.OldPassword).NotEmpty();

            RuleFor(x => x.NewPassword).NotEmpty();

            RuleFor(x => x.ConfirmPassword).NotEmpty();

        }
    }
}
