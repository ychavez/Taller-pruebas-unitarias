using FluentValidation;
using MediatR;

namespace Ordering.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>
        (IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                // nos traemos el contexto de la validacion
                var context = new ValidationContext<TRequest>(request);

                //ejecutamos las validaciones de manera async
                var validationResults = await
                    Task.WhenAll(validators.Select(v => v.ValidateAsync(context)));

                //buscamos validaciones fallidas
                var failures = validationResults.SelectMany(r => r.Errors)
                    .Where(f => f is not null).ToList();


                if (failures.Any())
                    throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
