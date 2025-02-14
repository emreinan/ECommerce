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
using static Application.Features.OrderHistories.Constants.OrderHistoriesOperationClaims;

namespace Application.Features.OrderHistories.Commands.Delete;

public class DeleteOrderHistoryCommand : IRequest<DeletedOrderHistoryResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, OrderHistoriesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetOrderHistories"];

    public class DeleteOrderHistoryCommandHandler : IRequestHandler<DeleteOrderHistoryCommand, DeletedOrderHistoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderHistoryRepository _orderHistoryRepository;
        private readonly OrderHistoryBusinessRules _orderHistoryBusinessRules;

        public DeleteOrderHistoryCommandHandler(IMapper mapper, IOrderHistoryRepository orderHistoryRepository,
                                         OrderHistoryBusinessRules orderHistoryBusinessRules)
        {
            _mapper = mapper;
            _orderHistoryRepository = orderHistoryRepository;
            _orderHistoryBusinessRules = orderHistoryBusinessRules;
        }

        public async Task<DeletedOrderHistoryResponse> Handle(DeleteOrderHistoryCommand request, CancellationToken cancellationToken)
        {
            OrderHistory? orderHistory = await _orderHistoryRepository.GetAsync(predicate: oh => oh.Id == request.Id, cancellationToken: cancellationToken);
            await _orderHistoryBusinessRules.OrderHistoryShouldExistWhenSelected(orderHistory);

            await _orderHistoryRepository.DeleteAsync(orderHistory!);

            DeletedOrderHistoryResponse response = _mapper.Map<DeletedOrderHistoryResponse>(orderHistory);
            return response;
        }
    }
}