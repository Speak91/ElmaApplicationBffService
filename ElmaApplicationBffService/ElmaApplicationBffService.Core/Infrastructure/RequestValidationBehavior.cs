using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ElmaApplicationBffService.Core.Infrastructure;

public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
        List<ValidationFailure> list = (from f in _validators.Select((v) => v.Validate(context)).SelectMany((result) => result.Errors)
                                        where f != null
                                        select f).ToList();
        if (list.Count != 0)
        {
            throw new ValidationException(list);
        }

        return next();
    }
}
