using Application.Features.OrderHistories.Constants;
using Application.Features.OrderHistories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.OrderHistories.Constants.OrderHistoriesOperationClaims;

namespace Application.Features.OrderHistories.Queries.GetById;

public class GetByIdOrderHistoryQuery : IRequest<GetByIdOrderHistoryResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdOrderHistoryQueryHandler : IRequestHandler<GetByIdOrderHistoryQuery, GetByIdOrderHistoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderHistoryRepository _orderHistoryRepository;
        private readonly OrderHistoryBusinessRules _orderHistoryBusinessRules;

        public GetByIdOrderHistoryQueryHandler(IMapper mapper, IOrderHistoryRepository orderHistoryRepository, OrderHistoryBusinessRules orderHistoryBusinessRules)
        {
            _mapper = mapper;
            _orderHistoryRepository = orderHistoryRepository;
            _orderHistoryBusinessRules = orderHistoryBusinessRules;
        }

        public async Task<GetByIdOrderHistoryResponse> Handle(GetByIdOrderHistoryQuery request, CancellationToken cancellationToken)
        {
            OrderHistory? orderHistory = await _orderHistoryRepository.GetAsync(predicate: oh => oh.Id == request.Id, cancellationToken: cancellationToken);
            await _orderHistoryBusinessRules.OrderHistoryShouldExistWhenSelected(orderHistory);

            GetByIdOrderHistoryResponse response = _mapper.Map<GetByIdOrderHistoryResponse>(orderHistory);
            return response;
        }
    }
}