﻿using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Basket : Entity<Guid>
{
    public Guid UserId { get; set; }


    public virtual ICollection<BasketItem> BasketItems { get; set; } = default!;
    public virtual User User { get; set; }

}
