using Application.Features.Orders.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Orders.Rules;

public class OrderBusinessRules(IOrderRepository orderRepository,
                                IDiscountRepository discountRepository,
                                ILocalizationService localizationService) : BaseBusinessRules
{
    private async Task throwBusinessException(string messageKey)
    {
        string message = await localizationService.GetLocalizedAsync(messageKey, OrdersBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task OrderShouldExistWhenSelected(Order? order)
    {
        if (order is null)
            await throwBusinessException(OrdersBusinessMessages.OrderNotExists);
    }

    public async Task OrderIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Order? order = await orderRepository.GetAsync(
            predicate: o => o.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await OrderShouldExistWhenSelected(order);
    }
    public decimal CalculateTax(decimal totalAmount)
    {
        return totalAmount * 0.20m; // %20 KDV
    }
    public decimal CalculateShipping(decimal totalAmount)
    {
        return totalAmount >= 500 ? 0 : 50; // 500₺ üzeri kargo ücretsiz
    }

    public void BasketIsNullOrEmpty(Basket? basket)
    {
        if (basket is null || basket.BasketItems.Count is 0)
            throw new BusinessException(OrdersBusinessMessages.BasketEmpty);
    }
    public void ValidateDiscountAsync(Discount? discount, decimal totalAmount)
    {
        if (discount is null)
            throw new BusinessException("Gçersiz indirim kodu.");

        if (!discount.IsActive)
            throw new BusinessException("Discount is not active!");

        if (discount.StartDate > DateTime.UtcNow)
            throw new BusinessException("Discount is not started yet!");

        if (discount.EndDate < DateTime.UtcNow)
            throw new BusinessException("Discount is expired!");

        if (discount.MinOrderAmount.HasValue && discount.MinOrderAmount.Value > totalAmount)
            throw new BusinessException($"Bu kuponu kullanabilmek için minimum sipariş tutarı {discount.MinOrderAmount.Value:C} olmalıdır.");

        if (discount.UsageLimit <= 0)
            throw new BusinessException("Discount usage limit is exceeded!");
    }

    public async Task<(decimal DiscountAmount, Guid? DiscountId)> ApplyDiscountAsync(Guid? discountId, decimal orderTotalAmount)
    {
        if (!discountId.HasValue)
            return (0, null);

        var discount = await discountRepository.GetAsync(d => d.Id == discountId.Value);
        ValidateDiscountAsync(discount, orderTotalAmount);

        decimal discountAmount = discount!.Percentage.HasValue
            ? orderTotalAmount * (discount.Percentage.Value / 100)
            : discount.Amount;

        discount.UsageLimit -= 1;
        await discountRepository.UpdateAsync(discount);

        return (discountAmount, discount.Id);
    }

}