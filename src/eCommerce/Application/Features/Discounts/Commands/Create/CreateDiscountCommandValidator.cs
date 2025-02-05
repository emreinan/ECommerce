using FluentValidation;

namespace Application.Features.Discounts.Commands.Create;

public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
{
    public CreateDiscountCommandValidator()
    {
        RuleFor(c => c.Code).NotEmpty();
        RuleFor(c => c.Amount).NotEmpty();
        RuleFor(c => c.StartDate).NotEmpty();
        RuleFor(c => c.EndDate).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
    }
}