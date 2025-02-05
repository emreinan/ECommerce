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

namespace Application.Features.Discounts.Commands.Create;

public class CreateDiscountCommand : IRequest<CreatedDiscountResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required string Code { get; set; }
    public required decimal Amount { get; set; }
    public decimal? Percentage { get; set; }
    public decimal? MinOrderAmount { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required bool IsActive { get; set; }

    public string[] Roles => [Admin, Write, DiscountsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDiscounts"];

    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, CreatedDiscountResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDiscountRepository _discountRepository;
        private readonly DiscountBusinessRules _discountBusinessRules;

        public CreateDiscountCommandHandler(IMapper mapper, IDiscountRepository discountRepository,
                                         DiscountBusinessRules discountBusinessRules)
        {
            _mapper = mapper;
            _discountRepository = discountRepository;
            _discountBusinessRules = discountBusinessRules;
        }

        public async Task<CreatedDiscountResponse> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            Discount discount = _mapper.Map<Discount>(request);

            await _discountRepository.AddAsync(discount);

            CreatedDiscountResponse response = _mapper.Map<CreatedDiscountResponse>(discount);
            return response;
        }
    }
}