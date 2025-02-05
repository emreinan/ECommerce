using NArchitecture.Core.Application.Responses;

namespace Application.Features.ProductComments.Commands.Delete;

public class DeletedProductCommentResponse : IResponse
{
    public Guid Id { get; set; }
}