using Application.Features.OrderHistories.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.OrderHistories;

public class OrderHistoryManager : IOrderHistoryService
{
    private readonly IOrderHistoryRepository _orderHistoryRepository;
    private readonly OrderHistoryBusinessRules _orderHistoryBusinessRules;

    public OrderHistoryManager(IOrderHistoryRepository orderHistoryRepository, OrderHistoryBusinessRules orderHistoryBusinessRules)
    {
        _orderHistoryRepository = orderHistoryRepository;
        _orderHistoryBusinessRules = orderHistoryBusinessRules;
    }

    public async Task<OrderHistory?> GetAsync(
        Expression<Func<OrderHistory, bool>> predicate,
        Func<IQueryable<OrderHistory>, IIncludableQueryable<OrderHistory, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        OrderHistory? orderHistory = await _orderHistoryRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return orderHistory;
    }

    public async Task<IPaginate<OrderHistory>?> GetListAsync(
        Expression<Func<OrderHistory, bool>>? predicate = null,
        Func<IQueryable<OrderHistory>, IOrderedQueryable<OrderHistory>>? orderBy = null,
        Func<IQueryable<OrderHistory>, IIncludableQueryable<OrderHistory, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<OrderHistory> orderHistoryList = await _orderHistoryRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return orderHistoryList;
    }

    public async Task<OrderHistory> AddAsync(OrderHistory orderHistory)
    {
        OrderHistory addedOrderHistory = await _orderHistoryRepository.AddAsync(orderHistory);

        return addedOrderHistory;
    }

    public async Task<OrderHistory> UpdateAsync(OrderHistory orderHistory)
    {
        OrderHistory updatedOrderHistory = await _orderHistoryRepository.UpdateAsync(orderHistory);

        return updatedOrderHistory;
    }

    public async Task<OrderHistory> DeleteAsync(OrderHistory orderHistory, bool permanent = false)
    {
        OrderHistory deletedOrderHistory = await _orderHistoryRepository.DeleteAsync(orderHistory);

        return deletedOrderHistory;
    }
}
