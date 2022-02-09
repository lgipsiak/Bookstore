using Bookstore.DAL.Entities;
using Bookstore.Shared.DTO;
using FluentValidation;
using System.Linq;

namespace Bookstore.BLL.Validators
{
    public class AuthorQueryValidator : AbstractValidator<AuthorQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15 };
        private string[] allowedSortByColumns = { nameof(Author.FirstName),
                                                  nameof(Author.LastName) };
        public AuthorQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                    context.AddFailure("PageSize", $"Page size must be in [{string.Join(",", allowedPageSizes)}]");
            });

            RuleFor(x => x.SortBy).Must(value => string.IsNullOrEmpty(value) || allowedSortByColumns.Contains(value))
                                  .WithMessage($"Sort by is optional or must be in [{string.Join(",", allowedSortByColumns)}]");

        }

    }
}
