using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class DiscountRepository : EfRepositoryBase<Discount, Guid, BaseDbContext>, IDiscountRepository
{
    public DiscountRepository(BaseDbContext context) : base(context)
    {
    }
}