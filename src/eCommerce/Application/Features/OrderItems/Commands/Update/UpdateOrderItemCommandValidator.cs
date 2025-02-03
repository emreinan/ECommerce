using FluentValidation;

namespace Application.Features.OrderItems.Commands.Update;

public class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
{
    public UpdateOrderItemCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.OrderId).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
        RuleFor(c => c.ProductNameAtOrderTime).NotEmpty();
        RuleFor(c => c.ProductPriceAtOrderTime).NotEmpty();
        RuleFor(c => c.Quantity).NotEmpty();
    }
}