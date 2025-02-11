using FluentValidation;

namespace Application.Features.OrderHistories.Commands.Delete;

public class DeleteOrderHistoryCommandValidator : AbstractValidator<DeleteOrderHistoryCommand>
{
    public DeleteOrderHistoryCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}