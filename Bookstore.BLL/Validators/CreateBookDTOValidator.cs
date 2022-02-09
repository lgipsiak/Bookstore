using Bookstore.Shared.DTO;
using FluentValidation;

namespace Bookstore.BLL.Validators
{
    public class CreateBookDTOValidator : AbstractValidator<CreateBookDTO>
    {
        public CreateBookDTOValidator()
        {
            RuleFor(x => x.Title).NotEmpty();

            RuleFor(x => x.AuthorIds).NotEmpty();

            RuleFor(x => x.TagIds).NotEmpty();
        }
    }
}
