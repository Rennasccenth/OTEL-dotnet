using System.Collections.Immutable;
using FluentValidation;
using FluentValidation.Results;
using Mediation.Configuration;
using MediatR;
using Microsoft.Extensions.Options;

namespace Mediation.PipelineBehaviours;

internal sealed class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> 
        where TRequest: class, IBaseRequest
{
    private readonly IEnumerable<IValidator<TRequest>> _requestValidators;
    private readonly MediationConfiguration _mediationConfigurationOptions;

    public ValidationPipelineBehavior(
        IEnumerable<IValidator<TRequest>> requestValidators,
        IOptions<MediationConfiguration> mediationConfigurationOptions)
    {
        _requestValidators = requestValidators;
        _mediationConfigurationOptions = mediationConfigurationOptions.Value;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        bool requestValidatorsExists = _requestValidators.Any();
        if (_mediationConfigurationOptions.FluentValidation.EnsureRequestHasValidator)
        {
            // Todo: Maybe build a Analyzer for this will be the best approach ever, no?
            FluentValidationMissConfigurationException.ThrowIf(
                condition: requestValidatorsExists is false, 
                message: $"No validator found for Request type of {typeof(TRequest)}. Did you forget to register the validator?");
        }

        if (requestValidatorsExists)
        {
            ValidationContext<TRequest> validationContext = new(request);
            IEnumerable<Task<ValidationResult>> validationTasks = _requestValidators
                .Select(validator => validator.ValidateAsync(validationContext, cancellationToken));
            
            ValidationResult[] validationResults = await Task.WhenAll(validationTasks);

            ImmutableArray<ValidationFailure> failures = [
                ..validationResults
                    .Where(validationResult => validationResult.Errors.Count != 0)
                    .SelectMany(validationResult => validationResult.Errors)
            ];

            if (failures.Length is not 0)
            {
                throw new ValidationException(failures);
            }
        }

        return await next();
    }
}

internal sealed class FluentValidationMissConfigurationException : Exception
{
    private FluentValidationMissConfigurationException(string message)
        : base(message) { }

    public static void ThrowIf(bool condition, string message)
    {
        if (condition is false) return;
        
        throw new FluentValidationMissConfigurationException(message);
    }
} 