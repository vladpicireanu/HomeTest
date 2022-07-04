using Application.Library.Queries;
using FluentValidation;

namespace Application.Validators.LibraryEndPoint
{
    public class GetBookReadRateRequestValidator : AbstractValidator<GetBookReadRateQuery>
    {
        public GetBookReadRateRequestValidator()
        {
            RuleFor(x => x.BookId)
                .NotEmpty()
                .WithMessage("Field BookId is missing or has default value. Expected: Positive integer.")
                .WithErrorCode("1006")
                .WithName("bookId");

            When(x => x.BookId != default, () =>
            {
                RuleFor(x => x.BookId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Bad value for BookId. Expected: Positive integer.")
                .WithErrorCode("1006.1")
                .WithName("bookId");

            });
        }
    }
}
