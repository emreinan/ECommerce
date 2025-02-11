using Application.Features.OrderHistories.Constants;
using Application.Features.OrderHistories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using Domain.Enums;
using static Application.Features.OrderHistories.Constants.OrderHistoriesOperationClaims;

namespace Application.Features.OrderHistories.Commands.Create;

public class CreateOrderHistoryCommand : IRequest<CreatedOrderHistoryResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required Guid OrderId { get; set; }
    public required OrderStatus Status { get; set; }
    public required DateTime ChangedAt { get; set; }
    public required string ChangedBy { get; set; }

    public string[] Roles => [Admin, Write, OrderHistoriesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetOrderHistories"];

    public class CreateOrderHistoryCommandHandler : IRequestHandler<CreateOrderHistoryCommand, CreatedOrderHistoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderHistoryRepository _orderHistoryRepository;
        private readonly OrderHistoryBusinessRules _orderHistoryBusinessRules;

        public CreateOrderHistoryCommandHandler(IMapper mapper, IOrderHistoryRepository orderHistoryRepository,
                                         OrderHistoryBusinessRules orderHistoryBusinessRules)
        {
            _mapper = mapper;
            _orderHistoryRepository = orderHistoryRepository;
            _orderHistoryBusinessRules = orderHistoryBusinessRules;
        }

        public async Task<CreatedOrderHistoryResponse> Handle(CreateOrderHistoryCommand request, CancellationToken cancellationToken)
        {
            OrderHistory orderHistory = _mapper.Map<OrderHistory>(request);

            await _orderHistoryRepository.AddAsync(orderHistory);

            CreatedOrderHistoryResponse response = _mapper.Map<CreatedOrderHistoryResponse>(orderHistory);
            return response;
        }
    }
}