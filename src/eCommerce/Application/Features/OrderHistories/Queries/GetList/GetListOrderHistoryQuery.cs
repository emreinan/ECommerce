using Application.Features.OrderHistories.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.OrderHistories.Constants.OrderHistoriesOperationClaims;

namespace Application.Features.OrderHistories.Queries.GetList;

public class GetListOrderHistoryQuery : IRequest<GetListResponse<GetListOrderHistoryListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListOrderHistories({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetOrderHistories";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListOrderHistoryQueryHandler : IRequestHandler<GetListOrderHistoryQuery, GetListResponse<GetListOrderHistoryListItemDto>>
    {
        private readonly IOrderHistoryRepository _orderHistoryRepository;
        private readonly IMapper _mapper;

        public GetListOrderHistoryQueryHandler(IOrderHistoryRepository orderHistoryRepository, IMapper mapper)
        {
            _orderHistoryRepository = orderHistoryRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListOrderHistoryListItemDto>> Handle(GetListOrderHistoryQuery request, CancellationToken cancellationToken)
        {
            IPaginate<OrderHistory> orderHistories = await _orderHistoryRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListOrderHistoryListItemDto> response = _mapper.Map<GetListResponse<GetListOrderHistoryListItemDto>>(orderHistories);
            return response;
        }
    }
}