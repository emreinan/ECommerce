using FluentValidation;

namespace Application.Features.Discounts.Commands.Update;

public class UpdateDiscountCommandValidator : AbstractValidator<UpdateDiscountCommand>
{
    public UpdateDiscountCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Code).NotEmpty();
        RuleFor(c => c.Amount).NotEmpty();
        RuleFor(c => c.StartDate).NotEmpty();
        RuleFor(c => c.EndDate).NotEmpty();
        RuleFor(c => c.IsActive).NotEmpty();
    }
}