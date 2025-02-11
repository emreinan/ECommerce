using NArchitecture.Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.OrderHistories.Queries.GetList;

public class GetListOrderHistoryListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime ChangedAt { get; set; }
    public string ChangedBy { get; set; }
}