using Application.StarterTasks.Queries;
using FluentValidation;

namespace Application.Validators.Starters
{
    public class GetReplicateStringRequestValidator : AbstractValidator<GetReplicateStringQuery>
    {
        public GetReplicateStringRequestValidator()
        {
            RuleFor(x => x.Replicas)
                .NotEmpty()
                .WithMessage("Field Replicas has default value. Expected: Positive integer.")
                .WithErrorCode("1002")
                .WithName("replicas");

            When(x => x.Replicas != default, () =>
            {
                RuleFor(x => x.Replicas)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Bad value for Replicas. Expected: Positive integer.")
                .WithErrorCode("1002.1")
                .WithName("replicas");

            });
        }
    }
}
