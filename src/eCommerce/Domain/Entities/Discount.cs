using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Discount : Entity<Guid>
{
    public string Code { get; set; } = default!; 
    public decimal Amount { get; set; } 
    public decimal? Percentage { get; set; } // Yüzdesel indirim 
    public decimal? MinOrderAmount { get; set; } 
    public int UsageLimit { get; set; }
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; } 
    public bool IsActive { get; set; } = true; 
}