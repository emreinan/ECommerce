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

namespace Application.Features.Discounts.Commands.Delete;

public class DeleteDiscountCommand : IRequest<DeletedDiscountResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, DiscountsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDiscounts"];

    public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, DeletedDiscountResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDiscountRepository _discountRepository;
        private readonly DiscountBusinessRules _discountBusinessRules;

        public DeleteDiscountCommandHandler(IMapper mapper, IDiscountRepository discountRepository,
                                         DiscountBusinessRules discountBusinessRules)
        {
            _mapper = mapper;
            _discountRepository = discountRepository;
            _discountBusinessRules = discountBusinessRules;
        }

        public async Task<DeletedDiscountResponse> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            Discount? discount = await _discountRepository.GetAsync(predicate: d => d.Id == request.Id, cancellationToken: cancellationToken);
            await _discountBusinessRules.DiscountShouldExistWhenSelected(discount);

            await _discountRepository.DeleteAsync(discount!);

            DeletedDiscountResponse response = _mapper.Map<DeletedDiscountResponse>(discount);
            return response;
        }
    }
}