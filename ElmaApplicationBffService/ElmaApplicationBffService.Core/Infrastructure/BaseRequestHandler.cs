using MediatR;

namespace ElmaApplicationBffService.Core.Infrastructure;

public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        return HandleAsync(request, cancellationToken);
    }

    public abstract Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}