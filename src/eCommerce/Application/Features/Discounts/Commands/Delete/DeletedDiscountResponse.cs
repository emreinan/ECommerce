using NArchitecture.Core.Application.Responses;

namespace Application.Features.Discounts.Commands.Delete;

public class DeletedDiscountResponse : IResponse
{
    public Guid Id { get; set; }
}