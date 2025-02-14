using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class OrderHistoryRepository(BaseDbContext context) : EfRepositoryBase<OrderHistory, Guid, BaseDbContext>(context), IOrderHistoryRepository
{
}