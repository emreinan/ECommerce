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
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required bool IsActive { get; set; }

    public string[] Roles => [Admin, Write, DiscountsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDiscounts"];

    public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, UpdatedDiscountResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDiscountRepository _discountRepository;
        private readonly DiscountBusinessRules _discountBusinessRules;

        public UpdateDiscountCommandHandler(IMapper mapper, IDiscountRepository discountRepository,
                                         DiscountBusinessRules discountBusinessRules)
        {
            _mapper = mapper;
            _discountRepository = discountRepository;
            _discountBusinessRules = discountBusinessRules;
        }

        public async Task<UpdatedDiscountResponse> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            Discount? discount = await _discountRepository.GetAsync(predicate: d => d.Id == request.Id, cancellationToken: cancellationToken);
            await _discountBusinessRules.DiscountShouldExistWhenSelected(discount);
            discount = _mapper.Map(request, discount);

            await _discountRepository.UpdateAsync(discount!);

            UpdatedDiscountResponse response = _mapper.Map<UpdatedDiscountResponse>(discount);
            return response;
        }
    }
}