using Application.Features.Orders.Constants;
using Application.Features.Orders.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Orders.Constants.OrdersOperationClaims;
using Domain.Enums;

namespace Application.Features.Orders.Commands.Update;

public class UpdateOrderCommand : IRequest<UpdatedOrderResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required Guid ShippingAddressId { get; set; }
    public required PaymentMethod PaymentMethod { get; set; }
    public required OrderStatus Status { get; set; }
    public required bool IsPaid { get; set; }

    public string[] Roles => [Admin, Write, OrdersOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetOrders"];

    public class UpdateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository,
                                     OrderBusinessRules orderBusinessRules) : IRequestHandler<UpdateOrderCommand, UpdatedOrderResponse>
    {
        public async Task<UpdatedOrderResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            Order? order = await orderRepository.GetAsync(predicate: o => o.Id == request.Id, cancellationToken: cancellationToken);
            await orderBusinessRules.OrderShouldExistWhenSelected(order);
            order = mapper.Map(request, order);

            await orderRepository.UpdateAsync(order!);

            UpdatedOrderResponse response = mapper.Map<UpdatedOrderResponse>(order);
            return response;
        }
    }
}