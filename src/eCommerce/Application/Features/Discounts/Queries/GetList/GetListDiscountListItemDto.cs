using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Discounts.Queries.GetList;

public class GetListDiscountListItemDto : IDto
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