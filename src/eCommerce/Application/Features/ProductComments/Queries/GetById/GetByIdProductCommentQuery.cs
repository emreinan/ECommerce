using Application.Features.ProductComments.Constants;
using Application.Features.ProductComments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ProductComments.Constants.ProductCommentsOperationClaims;

namespace Application.Features.ProductComments.Queries.GetById;

public class GetByIdProductCommentQuery : IRequest<GetByIdProductCommentResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdProductCommentQueryHandler : IRequestHandler<GetByIdProductCommentQuery, GetByIdProductCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductCommentRepository _productCommentRepository;
        private readonly ProductCommentBusinessRules _productCommentBusinessRules;

        public GetByIdProductCommentQueryHandler(IMapper mapper, IProductCommentRepository productCommentRepository, ProductCommentBusinessRules productCommentBusinessRules)
        {
            _mapper = mapper;
            _productCommentRepository = productCommentRepository;
            _productCommentBusinessRules = productCommentBusinessRules;
        }

        public async Task<GetByIdProductCommentResponse> Handle(GetByIdProductCommentQuery request, CancellationToken cancellationToken)
        {
            ProductComment? productComment = await _productCommentRepository.GetAsync(predicate: pc => pc.Id == request.Id, cancellationToken: cancellationToken);
            await _productCommentBusinessRules.ProductCommentShouldExistWhenSelected(productComment);

            GetByIdProductCommentResponse response = _mapper.Map<GetByIdProductCommentResponse>(productComment);
            return response;
        }
    }
}