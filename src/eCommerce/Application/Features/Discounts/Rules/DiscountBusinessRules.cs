using Application.Features.Discounts.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Discounts.Rules;

public class DiscountBusinessRules : BaseBusinessRules
{
    private readonly IDiscountRepository _discountRepository;
    private readonly ILocalizationService _localizationService;

    public DiscountBusinessRules(IDiscountRepository discountRepository, ILocalizationService localizationService)
    {
        _discountRepository = discountRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, DiscountsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task DiscountShouldExistWhenSelected(Discount? discount)
    {
        if (discount == null)
            await throwBusinessException(DiscountsBusinessMessages.DiscountNotExists);
    }

    public async Task DiscountIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Discount? discount = await _discountRepository.GetAsync(
            predicate: d => d.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await DiscountShouldExistWhenSelected(discount);
    }
}