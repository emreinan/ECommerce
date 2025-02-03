
using Domain.Enums;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class OrderHistory : Entity<Guid>
{
    public Guid OrderId { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime ChangedAt { get; set; }
    public string ChangedBy { get; set; } // Admin veya sistem kullanıcısı

    public virtual Order Order { get; set; }
}