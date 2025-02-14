using FluentValidation;

namespace Application.Features.Discounts.Commands.Update;

public class UpdateDiscountCommandValidator : AbstractValidator<UpdateDiscountCommand>
{
    public UpdateDiscountCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Code)
                   .NotEmpty()
                   .MinimumLength(5)
                   .MaximumLength(20);

        RuleFor(c => c.Amount)
            .GreaterThan(0)
            .When(c => !c.Percentage.HasValue || c.Percentage == 0)
            .WithMessage("Either Amount or Percentage must be specified.");

        RuleFor(c => c.Percentage)
            .GreaterThan(0)
            .LessThanOrEqualTo(100)
            .When(c => c.Percentage.HasValue)
            .WithMessage("Percentage must be between 0 and 100.");

        RuleFor(c => c.MinOrderAmount)
            .GreaterThan(0).When(c => c.MinOrderAmount.HasValue);

        RuleFor(c => c.UsageLimit)
             .Cascade(CascadeMode.Stop)
             .NotEmpty()
             .GreaterThanOrEqualTo(0);

        RuleFor(c => c.StartDate)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Start date cannot be in the past.");

        RuleFor(c => c.EndDate)
            .NotEmpty();

        RuleFor(c => c)
            .Must(c => c.StartDate < c.EndDate)
            .WithMessage("Start date must be less than end date.");

    }
}