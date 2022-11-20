using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CodingExercise.Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    var failureGroups = failures
                        .GroupBy(e => e.PropertyName, e => e.ErrorMessage);
                    var validationFailures = new List<ValidationFailure>();

                    foreach (var failureGroup in failureGroups)
                    {
                        var propertyName = failureGroup.Key;
                        var propertyFailures = failureGroup.ToArray();

                        validationFailures.Add(new ValidationFailure(propertyName, string.Concat(propertyFailures)));
                    }

                    throw new ValidationException(validationFailures);

                }
            }
            return await next();
        }
    }
}