using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviours
{
    public class ValidatorBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ValidatorBehaviour<TRequest, TResponse>> logger;
        private readonly IValidator<TRequest>[] validators;

        public ValidatorBehaviour(IValidator<TRequest>[] validators, ILogger<ValidatorBehaviour<TRequest, TResponse>> logger)
        {
            this.validators = validators;
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var typeName = request.GetType().Name;

            logger.LogInformation("----- Validating command {CommandType}", typeName);

            var failures = validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                logger.LogWarning("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}", typeName, request, failures);

                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
