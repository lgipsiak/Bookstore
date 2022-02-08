using FluentValidation;

namespace Bookstore.Shared.DTO.Validators
{
    public class CreateAuthorDTOValidator : AbstractValidator<CreateAuthorDTO>
    {
        public CreateAuthorDTOValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();

            RuleFor(x => x.LastName).NotEmpty();
        }
    }
}
