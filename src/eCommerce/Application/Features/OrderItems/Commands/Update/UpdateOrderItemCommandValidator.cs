using FluentValidation;

namespace Application.Features.OrderItems.Commands.Update;

public class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
{
    public UpdateOrderItemCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.OrderId).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();

        RuleFor(c => c.ProductPriceAtOrderTime)
            .NotEmpty()
            .GreaterThan(0).WithMessage("Product price must be greater than zero.");

        RuleFor(c => c.ProductNameAtOrderTime)
            .NotEmpty()
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

        RuleFor(c => c.Quantity)
            .NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(10).WithMessage("You cannot order more than 10 items at once.");

    }
}