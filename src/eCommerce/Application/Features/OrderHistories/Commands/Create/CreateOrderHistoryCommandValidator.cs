using FluentValidation;

namespace Application.Features.OrderHistories.Commands.Create;

public class CreateOrderHistoryCommandValidator : AbstractValidator<CreateOrderHistoryCommand>
{
    public CreateOrderHistoryCommandValidator()
    {
        RuleFor(c => c.OrderId).NotEmpty();
        RuleFor(c => c.Status).NotEmpty();
        RuleFor(c => c.ChangedAt).NotEmpty();
        RuleFor(c => c.ChangedBy).NotEmpty();
    }
}