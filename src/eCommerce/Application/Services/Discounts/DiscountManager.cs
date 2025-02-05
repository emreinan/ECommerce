using Application.Features.Discounts.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Discounts;

public class DiscountManager : IDiscountService
{
    private readonly IDiscountRepository _discountRepository;
    private readonly DiscountBusinessRules _discountBusinessRules;

    public DiscountManager(IDiscountRepository discountRepository, DiscountBusinessRules discountBusinessRules)
    {
        _discountRepository = discountRepository;
        _discountBusinessRules = discountBusinessRules;
    }

    public async Task<Discount?> GetAsync(
        Expression<Func<Discount, bool>> predicate,
        Func<IQueryable<Discount>, IIncludableQueryable<Discount, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Discount? discount = await _discountRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return discount;
    }

    public async Task<IPaginate<Discount>?> GetListAsync(
        Expression<Func<Discount, bool>>? predicate = null,
        Func<IQueryable<Discount>, IOrderedQueryable<Discount>>? orderBy = null,
        Func<IQueryable<Discount>, IIncludableQueryable<Discount, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Discount> discountList = await _discountRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return discountList;
    }

    public async Task<Discount> AddAsync(Discount discount)
    {
        Discount addedDiscount = await _discountRepository.AddAsync(discount);

        return addedDiscount;
    }

    public async Task<Discount> UpdateAsync(Discount discount)
    {
        Discount updatedDiscount = await _discountRepository.UpdateAsync(discount);

        return updatedDiscount;
    }

    public async Task<Discount> DeleteAsync(Discount discount, bool permanent = false)
    {
        Discount deletedDiscount = await _discountRepository.DeleteAsync(discount);

        return deletedDiscount;
    }
}
