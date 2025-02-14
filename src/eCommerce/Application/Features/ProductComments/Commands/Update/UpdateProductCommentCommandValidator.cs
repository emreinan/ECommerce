using FluentValidation;

namespace Application.Features.ProductComments.Commands.Update;

public class UpdateProductCommentCommandValidator : AbstractValidator<UpdateProductCommentCommand>
{
    public UpdateProductCommentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.Text)
                            .NotEmpty()
                            .MaximumLength(500).WithMessage("Comment text cannot exceed 500 characters.");
        RuleFor(c => c.StarCount)
            .Must(x => x >= 1 && x <= 5)
            .WithMessage("Star rating must be between 1 and 5.");
    }
}