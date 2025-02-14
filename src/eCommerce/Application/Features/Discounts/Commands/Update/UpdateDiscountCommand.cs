using Application.Features.Discounts.Constants;
using Application.Features.Discounts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Discounts.Constants.DiscountsOperationClaims;

namespace Application.Features.Discounts.Commands.Update;

public class UpdateDiscountCommand : IRequest<UpdatedDiscountResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required string Code { get; set; }
    public required decimal Amount { get; set; }
    public decimal? Percentage { get; set; }
    public decimal? MinOrderAmount { get; set; }
    public int UsageLimit { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required bool IsActive { get; set; }


    public string[] Roles => [Admin, Write, DiscountsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDiscounts"];

    public class UpdateDiscountCommandHandler(IMapper mapper, IDiscountRepository discountRepository,
                                     DiscountBusinessRules discountBusinessRules) : IRequestHandler<UpdateDiscountCommand, UpdatedDiscountResponse>
    {
        public async Task<UpdatedDiscountResponse> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            Discount? discount = await discountRepository.GetAsync(predicate: d => d.Id == request.Id, cancellationToken: cancellationToken);
            await discountBusinessRules.DiscountShouldExistWhenSelected(discount);
            discount = mapper.Map(request, discount);

            await discountRepository.UpdateAsync(discount!);

            UpdatedDiscountResponse response = mapper.Map<UpdatedDiscountResponse>(discount);
            return response;
        }
    }
}