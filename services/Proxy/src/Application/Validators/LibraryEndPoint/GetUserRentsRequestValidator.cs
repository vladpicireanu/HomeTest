using Application.Library.Queries;
using FluentValidation;

namespace Application.Validators.LibraryEndPoint
{
    public class GetUserRentsRequestValidator : AbstractValidator<GetUserRentsQuery>
    {
        public GetUserRentsRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("Field UserId is missing or has default value. Expected: Positive integer.")
                .WithErrorCode("1009")
                .WithName("userId");

            When(x => x.UserId != default, () =>
            {
                RuleFor(x => x.UserId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Bad value for UserId. Expected: Positive integer.")
                .WithErrorCode("1009.1")
                .WithName("userId");

            });
        }
    }
}
