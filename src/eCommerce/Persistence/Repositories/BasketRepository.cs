using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BasketRepository(BaseDbContext context) : EfRepositoryBase<Basket, Guid, BaseDbContext>(context), IBasketRepository
{
}