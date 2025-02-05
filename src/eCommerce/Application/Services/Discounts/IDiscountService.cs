using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Discounts;

public interface IDiscountService
{
    Task<Discount?> GetAsync(
        Expression<Func<Discount, bool>> predicate,
        Func<IQueryable<Discount>, IIncludableQueryable<Discount, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Discount>?> GetListAsync(
        Expression<Func<Discount, bool>>? predicate = null,
        Func<IQueryable<Discount>, IOrderedQueryable<Discount>>? orderBy = null,
        Func<IQueryable<Discount>, IIncludableQueryable<Discount, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Discount> AddAsync(Discount discount);
    Task<Discount> UpdateAsync(Discount discount);
    Task<Discount> DeleteAsync(Discount discount, bool permanent = false);
}
