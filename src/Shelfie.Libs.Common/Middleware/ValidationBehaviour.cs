using FluentValidation;
using MediatR;

namespace Shelfie.Libs.Common.Middleware;

internal sealed class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(
        IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken ct)
    {
        if (_validators.Any() is false)
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        var firstError = _validators
            .Select(_ => _.Validate(context))
            .FirstOrDefault(_ => _.IsValid is false)
            ?.Errors
            ?.FirstOrDefault();

        if (firstError is not null)
        {
            throw new ValidationException(firstError.ErrorMessage);
        }

        return await next();
    }
}
