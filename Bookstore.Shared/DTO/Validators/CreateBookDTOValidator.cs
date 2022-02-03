using FluentValidation;

namespace Bookstore.Shared.DTO.Validators
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
