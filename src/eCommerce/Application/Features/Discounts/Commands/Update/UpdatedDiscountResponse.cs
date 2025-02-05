using NArchitecture.Core.Application.Responses;

namespace Application.Features.Discounts.Commands.Update;

public class UpdatedDiscountResponse : IResponse
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public decimal Amount { get; set; }
    public decimal? Percentage { get; set; }
    public decimal? MinOrderAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
}