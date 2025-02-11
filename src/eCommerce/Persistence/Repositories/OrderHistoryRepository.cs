using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class OrderHistoryRepository : EfRepositoryBase<OrderHistory, Guid, BaseDbContext>, IOrderHistoryRepository
{
    public OrderHistoryRepository(BaseDbContext context) : base(context)
    {
    }
}