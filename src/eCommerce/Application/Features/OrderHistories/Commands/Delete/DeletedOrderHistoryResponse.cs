using NArchitecture.Core.Application.Responses;

namespace Application.Features.OrderHistories.Commands.Delete;

public class DeletedOrderHistoryResponse : IResponse
{
    public Guid Id { get; set; }
}