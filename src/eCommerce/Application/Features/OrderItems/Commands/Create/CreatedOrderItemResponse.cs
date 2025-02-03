using NArchitecture.Core.Application.Responses;

namespace Application.Features.OrderItems.Commands.Create;

public class CreatedOrderItemResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductNameAtOrderTime { get; set; }
    public decimal ProductPriceAtOrderTime { get; set; }
    public int Quantity { get; set; }
}