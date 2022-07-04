using Application.Library.Queries;
using FluentValidation;

namespace Application.Validators.LibraryEndPoint
{
    public class GetUsersWithMostRentsRequestValidator : AbstractValidator<GetUsersWithMostRentsQuery>
    {
        public GetUsersWithMostRentsRequestValidator()
        {
            RuleFor(x => x.TopRange)
                .NotEmpty()
                .WithMessage("Field TopRange is missing or has default value. Expected: Positive integer.")
                .WithErrorCode("1010")
                .WithName("topRange");

            When(x => x.TopRange != default, () =>
            {
                RuleFor(x => x.TopRange)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Bad value for TopRange. Expected: Positive integer.")
                .WithErrorCode("1010.1")
                .WithName("topRange");

            });

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("Field StartDate is missing. Expected: Date not bigger than today.")
                .WithErrorCode("1010.2")
                .WithName("startDate");

            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(DateTimeOffset.Now)
                .WithMessage("Bad value for StartDate. Expected: Date not bigger than today.")
                .WithErrorCode("1010.3")
                .WithName("startDate");

            RuleFor(x => x.ReturnDate)
                .NotEmpty()
                .WithMessage("Field ReturnDate is missing. Expected: ReturnDate to be greater than StartDate.")
                .WithErrorCode("1010.2")
                .WithName("returnDate");

            RuleFor(x => x.ReturnDate)
                .Must((model, returnDate) => returnDate > model.StartDate)
                .WithMessage("Bad value for ReturnDate. Expected: ReturnDate to be greater than StartDate.")
                .WithErrorCode("1003.4")
                .WithName("returnDate");
        }
    }
}
