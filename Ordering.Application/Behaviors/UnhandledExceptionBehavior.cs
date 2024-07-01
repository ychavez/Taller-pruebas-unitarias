using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Behaviors
{
    public class UnhandledExceptionBehavior<TRequest, TResponse>
        (ILogger<TRequest> logger)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {

                logger.LogError(ex,
                    $"Exepcion no controlada para la peticion {typeof(TRequest).Name}");
                throw;

            }
        }
    }
}
