using Application.Features.Discounts.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Discounts.Constants.DiscountsOperationClaims;

namespace Application.Features.Discounts.Queries.GetList;

public class GetListDiscountQuery : IRequest<GetListResponse<GetListDiscountListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListDiscounts({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetDiscounts";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListDiscountQueryHandler : IRequestHandler<GetListDiscountQuery, GetListResponse<GetListDiscountListItemDto>>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public GetListDiscountQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListDiscountListItemDto>> Handle(GetListDiscountQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Discount> discounts = await _discountRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListDiscountListItemDto> response = _mapper.Map<GetListResponse<GetListDiscountListItemDto>>(discounts);
            return response;
        }
    }
}