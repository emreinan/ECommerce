using NArchitecture.Core.Application.Responses;

namespace Application.Features.Products.Commands.Update;

public class UpdatedProductResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid SellerId { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? Details { get; set; }
    public int StockAmount { get; set; }
    public bool Enabled { get; set; }
}