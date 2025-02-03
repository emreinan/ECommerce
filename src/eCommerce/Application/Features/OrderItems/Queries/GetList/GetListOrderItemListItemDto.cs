using NArchitecture.Core.Application.Dtos;

namespace Application.Features.OrderItems.Queries.GetList;

public class GetListOrderItemListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductNameAtOrderTime { get; set; }
    public decimal ProductPriceAtOrderTime { get; set; }
    public int Quantity { get; set; }
}