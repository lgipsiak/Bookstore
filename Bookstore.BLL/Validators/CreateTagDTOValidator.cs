using Bookstore.DAL;
using Bookstore.Shared.DTO;
using FluentValidation;
using System.Linq;

namespace Bookstore.BLL.Validators
{
    public class CreateTagDTOValidator : AbstractValidator<CreateTagDTO>
    {
        public CreateTagDTOValidator(BookstoreDbContext dbContext)
        {
            RuleFor(x => x.Message).NotEmpty()
                                   .Custom((value, context) =>
                                   {
                                       var messageInUse = dbContext.Tags.Any(x => x.Message == value);

                                       if (messageInUse)
                                           context.AddFailure("Message", "This category already exists.");
                                   });
        }
    }
}
