using AutoMapper;
using MediatR;
using Ordering.Application.Contracts;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Commands.Checkout
{
    public class CheckoutOrderCommandHandler(IGenericRepository<Order> repository, IMapper mapper) 
        : IRequestHandler<CheckoutOrderCommand, int>
    {
        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var order = mapper.Map<Order>(request);
            var newOrder = await repository.AddAsync(order);
            return newOrder.Id;

        }
    }
}
