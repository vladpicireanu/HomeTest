using Application.StarterTasks.Queries;
using FluentValidation;

namespace Application.Validators.Starters
{
    public class IsNumberTwoPowerRequestValidator : AbstractValidator<IsNumberTwoPowerQuery>
    {
        public IsNumberTwoPowerRequestValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty()
                .WithMessage("Field Replicas has default value. Expected: Positive integer.")
                .WithErrorCode("1001")
                .WithName("number");

            When(x => x.Number != default, () =>
            {
                RuleFor(x => x.Number)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Bad value for Replicas. Expected: Positive integer.")
                .WithErrorCode("1001.1")
                .WithName("number");

            });
        }
    }
}
