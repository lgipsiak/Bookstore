using Bookstore.Shared.DTO;
using FluentValidation;

namespace Bookstore.BLL.Validators
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
