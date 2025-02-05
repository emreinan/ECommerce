using Application.Features.ProductComments.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.ProductComments.Rules;

public class ProductCommentBusinessRules : BaseBusinessRules
{
    private readonly IProductCommentRepository _productCommentRepository;
    private readonly ILocalizationService _localizationService;

    public ProductCommentBusinessRules(IProductCommentRepository productCommentRepository, ILocalizationService localizationService)
    {
        _productCommentRepository = productCommentRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ProductCommentsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ProductCommentShouldExistWhenSelected(ProductComment? productComment)
    {
        if (productComment == null)
            await throwBusinessException(ProductCommentsBusinessMessages.ProductCommentNotExists);
    }

    public async Task ProductCommentIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ProductComment? productComment = await _productCommentRepository.GetAsync(
            predicate: pc => pc.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ProductCommentShouldExistWhenSelected(productComment);
    }
}