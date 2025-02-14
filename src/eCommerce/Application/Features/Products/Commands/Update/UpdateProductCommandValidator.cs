using FluentValidation;

namespace Application.Features.Products.Commands.Update;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.SellerId).NotEmpty();
        RuleFor(c => c.CategoryId).NotEmpty();
        RuleFor(c => c.Name)
             .NotEmpty()
             .MaximumLength(200).WithMessage("Product name must be at most 200 characters.");
        RuleFor(c => c.Price)
            .NotEmpty()
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
        RuleFor(c => c.StockAmount)
            .NotEmpty()
            .GreaterThanOrEqualTo(0).WithMessage("Stock amount cannot be negative.");
    }
}