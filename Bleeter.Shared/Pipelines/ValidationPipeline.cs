using Bleeter.Shared.Exceptions;
using FluentValidation;
using MediatR;

namespace Bleeter.Shared.Pipelines;

public sealed class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipeline(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        var validationErrors = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context, cancellationToken)));
        var errors = validationErrors
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .GroupBy(x => x.PropertyName, t => t.ErrorMessage, (s, enumerable) => new
            {
                Key = s,
                Errors = enumerable.Distinct().ToList()
            })
            .ToDictionary(x => x.Key, t => t.Errors);

        if (errors.Count != 0)
        {
            throw new ValidatorException(errors);
        }

        return await next();
    }
}