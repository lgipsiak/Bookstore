using Bookstore.DAL;
using FluentValidation;

namespace Bookstore.Shared.DTO.Validators
{
    public class CreateAuthorDTOValidator : AbstractValidator<CreateAuthorDTO>
    {
        public CreateAuthorDTOValidator(BookstoreDbContext dbContext)
        {
            RuleFor(x => x.FirstName).NotEmpty();

            RuleFor(x => x.LastName).NotEmpty();
        }
    }
}
