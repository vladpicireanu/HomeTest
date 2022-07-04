using Application.Library.Queries;
using FluentValidation;

namespace Application.Validators.LibraryEndPoint
{
    public class GetBookByIdRequestValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdRequestValidator()
        {
            RuleFor(x => x.BookId)
                .NotEmpty()
                .WithMessage("Field BookId is missing or has default value. Expected: Positive integer.")
                .WithErrorCode("1005")
                .WithName("bookId");

            When(x => x.BookId != default, () =>
            {
                RuleFor(x => x.BookId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Bad value for BookId. Expected: Positive integer.")
                .WithErrorCode("1005.1")
                .WithName("bookId");

            });
        }
    }
}
