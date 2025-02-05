using Application.Features.ProductComments.Constants;
using Application.Features.ProductComments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ProductComments.Constants.ProductCommentsOperationClaims;

namespace Application.Features.ProductComments.Commands.Update;

public class UpdateProductCommentCommand : IRequest<UpdatedProductCommentResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required Guid ProductId { get; set; }
    public required Guid UserId { get; set; }
    public required string Text { get; set; }
    public required byte StarCount { get; set; }
    public required bool IsConfirmed { get; set; }

    public string[] Roles => [Admin, Write, ProductCommentsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetProductComments"];

    public class UpdateProductCommentCommandHandler : IRequestHandler<UpdateProductCommentCommand, UpdatedProductCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductCommentRepository _productCommentRepository;
        private readonly ProductCommentBusinessRules _productCommentBusinessRules;

        public UpdateProductCommentCommandHandler(IMapper mapper, IProductCommentRepository productCommentRepository,
                                         ProductCommentBusinessRules productCommentBusinessRules)
        {
            _mapper = mapper;
            _productCommentRepository = productCommentRepository;
            _productCommentBusinessRules = productCommentBusinessRules;
        }

        public async Task<UpdatedProductCommentResponse> Handle(UpdateProductCommentCommand request, CancellationToken cancellationToken)
        {
            ProductComment? productComment = await _productCommentRepository.GetAsync(predicate: pc => pc.Id == request.Id, cancellationToken: cancellationToken);
            await _productCommentBusinessRules.ProductCommentShouldExistWhenSelected(productComment);
            productComment = _mapper.Map(request, productComment);

            await _productCommentRepository.UpdateAsync(productComment!);

            UpdatedProductCommentResponse response = _mapper.Map<UpdatedProductCommentResponse>(productComment);
            return response;
        }
    }
}