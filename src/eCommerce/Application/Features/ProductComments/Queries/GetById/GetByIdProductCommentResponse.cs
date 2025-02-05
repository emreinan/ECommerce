using NArchitecture.Core.Application.Responses;

namespace Application.Features.ProductComments.Queries.GetById;

public class GetByIdProductCommentResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; }
    public byte StarCount { get; set; }
    public bool IsConfirmed { get; set; }
}