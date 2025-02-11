using Application.Features.OrderHistories.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.OrderHistories.Rules;

public class OrderHistoryBusinessRules : BaseBusinessRules
{
    private readonly IOrderHistoryRepository _orderHistoryRepository;
    private readonly ILocalizationService _localizationService;

    public OrderHistoryBusinessRules(IOrderHistoryRepository orderHistoryRepository, ILocalizationService localizationService)
    {
        _orderHistoryRepository = orderHistoryRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, OrderHistoriesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task OrderHistoryShouldExistWhenSelected(OrderHistory? orderHistory)
    {
        if (orderHistory == null)
            await throwBusinessException(OrderHistoriesBusinessMessages.OrderHistoryNotExists);
    }

    public async Task OrderHistoryIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        OrderHistory? orderHistory = await _orderHistoryRepository.GetAsync(
            predicate: oh => oh.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await OrderHistoryShouldExistWhenSelected(orderHistory);
    }
}