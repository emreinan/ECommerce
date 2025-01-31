
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Order : Entity<Guid>
{
    public Guid UserId { get; set; }
    public string OrderCode { get; set; } = default!;
    public string Address { get; set; } = default!;


    //public virtual ICollection<OrderItem> OrderItems { get; set; } = default!;
    public virtual User User { get; set; } = default!;
}
