using FluentValidation;

namespace Application.Features.OrderHistories.Commands.Update;

public class UpdateOrderHistoryCommandValidator : AbstractValidator<UpdateOrderHistoryCommand>
{
    public UpdateOrderHistoryCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.OrderId).NotEmpty();
        RuleFor(c => c.Status).NotEmpty();
        RuleFor(c => c.ChangedAt).NotEmpty();
        RuleFor(c => c.ChangedBy).NotEmpty();
    }
}