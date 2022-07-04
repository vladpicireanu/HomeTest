using Application.Library.Queries;
using FluentValidation;

namespace Application.Validators.LibraryEndPoint
{
    public class GetMostBorrowedBooksRequestValidator : AbstractValidator<GetMostBorrowedBooksQuery>
    {
        public GetMostBorrowedBooksRequestValidator()
        {
            RuleFor(x => x.TopRange)
                .NotEmpty()
                .WithMessage("Field TopRange is missing or has default value. Expected: Positive integer.")
                .WithErrorCode("1007")
                .WithName("topRange");

            When(x => x.TopRange != default, () =>
            {
                RuleFor(x => x.TopRange)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Bad value for TopRange. Expected: Positive integer.")
                .WithErrorCode("1007.1")
                .WithName("topRange");

            });
        }
    }
}
