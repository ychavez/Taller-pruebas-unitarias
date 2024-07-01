using FluentValidation;

namespace Ordering.Application.Features.Commands.Checkout
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(x => x.Address).EmailAddress()
                .NotEmpty().WithMessage("La direccion no puede ser vacia oiga");

            RuleFor(x => x.UserName)
                .MinimumLength(2)
                .MaximumLength(100)
                .WithMessage("el maximo son 100");
        }
    }
}
