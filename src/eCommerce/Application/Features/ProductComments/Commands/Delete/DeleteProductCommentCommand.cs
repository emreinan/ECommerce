using Application.Features.ProductComments.Constants;
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

namespace Application.Features.ProductComments.Commands.Delete;

public class DeleteProductCommentCommand : IRequest<DeletedProductCommentResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, ProductCommentsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetProductComments"];

    public class DeleteProductCommentCommandHandler : IRequestHandler<DeleteProductCommentCommand, DeletedProductCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductCommentRepository _productCommentRepository;
        private readonly ProductCommentBusinessRules _productCommentBusinessRules;

        public DeleteProductCommentCommandHandler(IMapper mapper, IProductCommentRepository productCommentRepository,
                                         ProductCommentBusinessRules productCommentBusinessRules)
        {
            _mapper = mapper;
            _productCommentRepository = productCommentRepository;
            _productCommentBusinessRules = productCommentBusinessRules;
        }

        public async Task<DeletedProductCommentResponse> Handle(DeleteProductCommentCommand request, CancellationToken cancellationToken)
        {
            ProductComment? productComment = await _productCommentRepository.GetAsync(predicate: pc => pc.Id == request.Id, cancellationToken: cancellationToken);
            await _productCommentBusinessRules.ProductCommentShouldExistWhenSelected(productComment);

            await _productCommentRepository.DeleteAsync(productComment!);

            DeletedProductCommentResponse response = _mapper.Map<DeletedProductCommentResponse>(productComment);
            return response;
        }
    }
}