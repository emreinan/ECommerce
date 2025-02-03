using Domain.Enums;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Order : Entity<Guid>
{
    public Guid UserId { get; set; }
    public Guid ShippingAddressId { get; set; }

    public string OrderCode { get; set; } = default!;
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal DiscountAmount { get; set; }

    public OrderStatus Status { get; set; } 
    public bool IsPaid { get; set; } 
    public PaymentMethod PaymentMethod { get; set; }

    public virtual User User { get; set; } = default!;
    public virtual ICollection<OrderItem> OrderItems { get; set; } = default!;
    public virtual Address ShippingAddress { get; set; } = default!;

    public decimal CalculateFinalAmount() => (TotalAmount - DiscountAmount) + TaxAmount;
}
