using FluentValidation;

namespace Ordering.Application.Features.Queries
{
    public class GetOrdersListQueryValidator : AbstractValidator<GetOrdersListQuery>
    {
        public GetOrdersListQueryValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(2);
        }
    }
}
