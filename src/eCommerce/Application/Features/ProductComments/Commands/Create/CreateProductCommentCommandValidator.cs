using FluentValidation;

namespace Application.Features.ProductComments.Commands.Create;

public class CreateProductCommentCommandValidator : AbstractValidator<CreateProductCommentCommand>
{
    public CreateProductCommentCommandValidator()
    {
        RuleFor(c => c.ProductId).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.Text).NotEmpty();
        RuleFor(c => c.StarCount).NotEmpty();
        RuleFor(c => c.IsConfirmed).NotEmpty();
    }
}