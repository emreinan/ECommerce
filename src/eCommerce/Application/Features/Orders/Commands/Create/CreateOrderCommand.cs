using Application.Features.Orders.Constants;
using Application.Features.Orders.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using Domain.Enums;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Orders.Constants.OrdersOperationClaims;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Application.Features.Orders.Commands.Create;

public class CreateOrderCommand : IRequest<CreatedOrderResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required Guid UserId { get; set; }
    public required Guid ShippingAddressId { get; set; }
    public Guid? DiscountId { get; set; }
    public required PaymentMethod PaymentMethod { get; set; }


    public string[] Roles => [Admin, Write, OrdersOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetOrders"];

    public class CreateOrderCommandHandler(IOrderRepository orderRepository,
                                     IBasketRepository basketRepository,
                                     OrderBusinessRules orderBusinessRules) : IRequestHandler<CreateOrderCommand, CreatedOrderResponse>
    {
        public async Task<CreatedOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetAsync(b => b.UserId == request.UserId,
                                                        include: b => b.Include(bi => bi.BasketItems),
                                                        cancellationToken: cancellationToken);

            orderBusinessRules.BasketIsNullOrEmpty(basket);

            var orderItems = basket!.BasketItems.Select(bi => new OrderItem
            {
                ProductId = bi.ProductId,
                ProductNameAtOrderTime = bi.Product.Name,
                ProductPriceAtOrderTime = bi.Product.Price,
                Quantity = bi.Quantity,
            }).ToList();

            decimal totalAmount = orderItems.Sum(oi => oi.TotalPrice);

            var (discountAmount, discountId) = await orderBusinessRules.ApplyDiscountAsync(request.DiscountId, totalAmount);

            decimal taxAmount = orderBusinessRules.CalculateTax(totalAmount);
            decimal shippingCost = orderBusinessRules.CalculateShipping(taxAmount);
            decimal finalAmount = (totalAmount - discountAmount) + taxAmount + shippingCost;

            Order order = new Order
            {
                UserId = request.UserId,
                ShippingAddressId = request.ShippingAddressId,
                DiscountId = discountId,
                OrderCode = $"ORD-{Guid.NewGuid().ToString()[..8].ToUpper()}",
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                IsPaid = false, // Ödeme tamamlandýktan sonra güncellenecek
                PaymentMethod = request.PaymentMethod,
                TaxAmount = taxAmount,
                ShippingCost = shippingCost,
                FinalAmount = finalAmount,
                OrderItems = orderItems
            };

            await orderRepository.AddAsync(order, cancellationToken);
            await basketRepository.DeleteAsync(basket, cancellationToken: cancellationToken);
            return new CreatedOrderResponse
            {
                Id = order.Id,
                OrderCode = order.OrderCode,
                TotalAmount = totalAmount,
                DiscountAmount = discountAmount,
                TaxAmount = taxAmount,
                ShippingCost = shippingCost,
                FinalAmount = order.FinalAmount,
                OrderDate = order.OrderDate,
                PaymentMethod = order.PaymentMethod.ToString(),
                ShippingAddress = $"{order.ShippingAddress.Street}, {order.ShippingAddress.State}, {order.ShippingAddress.City} - {order.ShippingAddress.Country}",
                Status = order.Status.ToString()
            };
        }
    }
}