using Application.Features.ProductComments.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.ProductComments.Constants.ProductCommentsOperationClaims;

namespace Application.Features.ProductComments.Queries.GetList;

public class GetListProductCommentQuery : IRequest<GetListResponse<GetListProductCommentListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListProductComments({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetProductComments";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListProductCommentQueryHandler : IRequestHandler<GetListProductCommentQuery, GetListResponse<GetListProductCommentListItemDto>>
    {
        private readonly IProductCommentRepository _productCommentRepository;
        private readonly IMapper _mapper;

        public GetListProductCommentQueryHandler(IProductCommentRepository productCommentRepository, IMapper mapper)
        {
            _productCommentRepository = productCommentRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListProductCommentListItemDto>> Handle(GetListProductCommentQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ProductComment> productComments = await _productCommentRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListProductCommentListItemDto> response = _mapper.Map<GetListResponse<GetListProductCommentListItemDto>>(productComments);
            return response;
        }
    }
}