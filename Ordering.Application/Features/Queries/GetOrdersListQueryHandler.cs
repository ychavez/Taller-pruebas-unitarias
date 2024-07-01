using AutoMapper;
using MediatR;
using Ordering.Application.Contracts;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Queries
{
    public class GetOrdersListQueryHandler(IGenericRepository<Order> repository, IMapper mapper)
        : IRequestHandler<GetOrdersListQuery, List<OrdersViewModel>>
    {
        public async Task<List<OrdersViewModel>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orders = await repository.GetAsync(x => x.UserName == request.UserName);
            return mapper.Map<List<OrdersViewModel>>(orders);
        }
    }
}
