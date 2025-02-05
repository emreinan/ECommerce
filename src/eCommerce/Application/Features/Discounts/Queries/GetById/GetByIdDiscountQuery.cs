using Application.Features.Discounts.Constants;
using Application.Features.Discounts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Discounts.Constants.DiscountsOperationClaims;

namespace Application.Features.Discounts.Queries.GetById;

public class GetByIdDiscountQuery : IRequest<GetByIdDiscountResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdDiscountQueryHandler : IRequestHandler<GetByIdDiscountQuery, GetByIdDiscountResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDiscountRepository _discountRepository;
        private readonly DiscountBusinessRules _discountBusinessRules;

        public GetByIdDiscountQueryHandler(IMapper mapper, IDiscountRepository discountRepository, DiscountBusinessRules discountBusinessRules)
        {
            _mapper = mapper;
            _discountRepository = discountRepository;
            _discountBusinessRules = discountBusinessRules;
        }

        public async Task<GetByIdDiscountResponse> Handle(GetByIdDiscountQuery request, CancellationToken cancellationToken)
        {
            Discount? discount = await _discountRepository.GetAsync(predicate: d => d.Id == request.Id, cancellationToken: cancellationToken);
            await _discountBusinessRules.DiscountShouldExistWhenSelected(discount);

            GetByIdDiscountResponse response = _mapper.Map<GetByIdDiscountResponse>(discount);
            return response;
        }
    }
}