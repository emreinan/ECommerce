using FluentValidation;

namespace Application.Features.ProductComments.Commands.Update;

public class UpdateProductCommentCommandValidator : AbstractValidator<UpdateProductCommentCommand>
{
    public UpdateProductCommentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.Text).NotEmpty();
        RuleFor(c => c.StarCount).NotEmpty();
        RuleFor(c => c.IsConfirmed).NotEmpty();
    }
}