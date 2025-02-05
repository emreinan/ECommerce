using NArchitecture.Core.Application.Responses;

namespace Application.Features.Orders.Commands.Create;

public class CreatedOrderResponse : IResponse
{
    public Guid Id { get; set; }
    public string OrderCode { get; set; } = default!;
    public decimal TotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal FinalAmount { get; set; }
    public string PaymentMethod { get; set; }
    public string ShippingAddress { get; set; } = default!;
    public string Status { get; set; }
    public DateTime OrderDate { get; set; }

}