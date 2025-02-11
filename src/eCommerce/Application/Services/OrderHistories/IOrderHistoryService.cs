using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.OrderHistories;

public interface IOrderHistoryService
{
    Task<OrderHistory?> GetAsync(
        Expression<Func<OrderHistory, bool>> predicate,
        Func<IQueryable<OrderHistory>, IIncludableQueryable<OrderHistory, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<OrderHistory>?> GetListAsync(
        Expression<Func<OrderHistory, bool>>? predicate = null,
        Func<IQueryable<OrderHistory>, IOrderedQueryable<OrderHistory>>? orderBy = null,
        Func<IQueryable<OrderHistory>, IIncludableQueryable<OrderHistory, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<OrderHistory> AddAsync(OrderHistory orderHistory);
    Task<OrderHistory> UpdateAsync(OrderHistory orderHistory);
    Task<OrderHistory> DeleteAsync(OrderHistory orderHistory, bool permanent = false);
}
