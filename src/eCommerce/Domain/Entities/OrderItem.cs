
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class OrderItem : Entity<Guid>
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }

    public string ProductNameAtOrderTime { get; set; } // O anki ürün isimi
    public decimal ProductPriceAtOrderTime { get; set; } // O anki fiyat
    public int Quantity { get; set; }
    public decimal TotalPrice => ProductPriceAtOrderTime * Quantity;

    public virtual Order Order { get; set; } = default!;
}