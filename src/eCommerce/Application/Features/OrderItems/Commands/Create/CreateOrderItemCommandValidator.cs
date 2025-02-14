using FluentValidation;

namespace Application.Features.OrderItems.Commands.Create;

public class CreateOrderItemCommandValidator : AbstractValidator<CreateOrderItemCommand>
{
    public CreateOrderItemCommandValidator()
    {
        RuleFor(c => c.OrderId).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();

        RuleFor(c => c.ProductPriceAtOrderTime)
            .NotEmpty()
            .GreaterThan(0).WithMessage("Product price must be greater than zero.");

        RuleFor(c => c.ProductNameAtOrderTime)
            .NotEmpty()
            .MaximumLength(200).WithMessage("Product name cannot exceed 200 characters.");

        RuleFor(c => c.Quantity)
            .NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(1000).WithMessage("You cannot order more than 1000 items at once.");
    }
}