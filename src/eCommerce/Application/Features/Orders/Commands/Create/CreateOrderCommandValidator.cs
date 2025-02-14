using FluentValidation;

namespace Application.Features.Orders.Commands.Create;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.ShippingAddressId).NotEmpty();
        RuleFor(x => x.PaymentMethod).NotEmpty();
        RuleFor(x => x.DiscountId)
            .NotEmpty()
            .When(x => x.DiscountId.HasValue) // Eðer indirim varsa, boþ olamaz
            .WithMessage("DiscountId cannot be empty if provided.");

    }
}