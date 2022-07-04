using Application.StarterTasks.Queries;
using FluentValidation;

namespace Application.Validators.Starters
{
    public class GetOddNumbersRequestValidator : AbstractValidator<GetOddNumbersQuery>
    {
        public GetOddNumbersRequestValidator()
        {
            RuleFor(x => x.StartNumber)
                .NotEmpty()
                .WithMessage("Field StartNumber has default value. Expected: Positive integer.")
                .WithErrorCode("1003")
                .WithName("startNumber");

            When(x => x.StartNumber != default, () =>
            {
                RuleFor(x => x.StartNumber)
                    .GreaterThanOrEqualTo(1)
                    .WithMessage("Bad value for StartNumber. Expected: Positive integer.")
                    .WithErrorCode("1003.1")
                    .WithName("startNumber");

            });

            RuleFor(x => x.StopNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Field StopNumber has default value. Expected: Positive integer.")
                .WithErrorCode("1003.2")
                .WithName("stopNumber");

            When(x => x.StopNumber != default, () =>
            {
                RuleFor(x => x.StopNumber)
                    .GreaterThanOrEqualTo(1)
                    .WithMessage("Bad value for StopNumber. Expected: Positive integer.")
                    .WithErrorCode("1003.3")
                    .WithName("stopNumber");

            });

            When(x => x.StartNumber > 0, () =>
            {
                RuleFor(x => x.StopNumber)
                    .Must((model, stopNumber) => stopNumber >= model.StartNumber)
                    .WithMessage("Bad value for StopNumber. Expected: StopNumber to be greater or equal than StartNumber.")
                    .WithErrorCode("1003.4")
                    .WithName("stopNumber");

            });
        }
    }
}
