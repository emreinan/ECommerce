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

namespace Application.Features.ProductComments.Commands.Create;

public class CreateProductCommentCommand : IRequest<CreatedProductCommentResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required Guid ProductId { get; set; }
    public required Guid UserId { get; set; }
    public required string Text { get; set; }
    public required byte StarCount { get; set; }
    public required bool IsConfirmed { get; set; }

    public string[] Roles => [Admin, Write, ProductCommentsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetProductComments"];

    public class CreateProductCommentCommandHandler : IRequestHandler<CreateProductCommentCommand, CreatedProductCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductCommentRepository _productCommentRepository;
        private readonly ProductCommentBusinessRules _productCommentBusinessRules;

        public CreateProductCommentCommandHandler(IMapper mapper, IProductCommentRepository productCommentRepository,
                                         ProductCommentBusinessRules productCommentBusinessRules)
        {
            _mapper = mapper;
            _productCommentRepository = productCommentRepository;
            _productCommentBusinessRules = productCommentBusinessRules;
        }

        public async Task<CreatedProductCommentResponse> Handle(CreateProductCommentCommand request, CancellationToken cancellationToken)
        {
            ProductComment productComment = _mapper.Map<ProductComment>(request);

            await _productCommentRepository.AddAsync(productComment);

            CreatedProductCommentResponse response = _mapper.Map<CreatedProductCommentResponse>(productComment);
            return response;
        }
    }
}