using MediatR;

namespace Ordering.Application.Features.Queries
{
    public class GetOrdersListQuery : IRequest<List<OrdersViewModel>>
    {
        public string UserName { get; set; } = null!;
    }
}
