using Bookstore.DAL;
using Bookstore.Shared.DTO;
using FluentValidation;
using System.Linq;

namespace Bookstore.BLL.Validators
{
    public class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
    {

        public RegisterUserDTOValidator(BookstoreDbContext dbContext)
        {
            RuleFor(x => x.FirstName).NotEmpty();

            RuleFor(x => x.LastName).NotEmpty();

            RuleFor(x => x.Email).NotEmpty()
                                 .EmailAddress()
                                 .Custom((value, context) =>
                                 {
                                     var emailInUse = dbContext.Users.Any(x => x.Email == value);

                                     if (emailInUse)
                                         context.AddFailure("Email", "This email is taken.");
                                 });


            RuleFor(x => x.Password).MinimumLength(8);

            RuleFor(x => x.ConfirmPasword).Equal(x => x.Password);
        }
    }
}
