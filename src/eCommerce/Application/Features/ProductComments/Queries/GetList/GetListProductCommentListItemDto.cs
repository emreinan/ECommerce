using NArchitecture.Core.Application.Dtos;

namespace Application.Features.ProductComments.Queries.GetList;

public class GetListProductCommentListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; }
    public byte StarCount { get; set; }
    public bool IsConfirmed { get; set; }
}