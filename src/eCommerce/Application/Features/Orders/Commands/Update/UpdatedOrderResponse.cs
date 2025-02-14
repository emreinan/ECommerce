using NArchitecture.Core.Application.Responses;

namespace Application.Features.Orders.Commands.Update;

public class UpdatedOrderResponse : IResponse
{
    public Guid UserId { get; set; }
    public string OrderCode { get; set; }
    public string Address { get; set; }
}