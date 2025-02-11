using NArchitecture.Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.OrderHistories.Commands.Update;

public class UpdatedOrderHistoryResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime ChangedAt { get; set; }
    public string ChangedBy { get; set; }
}