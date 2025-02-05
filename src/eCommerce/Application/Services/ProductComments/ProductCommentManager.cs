using Application.Features.ProductComments.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ProductComments;

public class ProductCommentManager : IProductCommentService
{
    private readonly IProductCommentRepository _productCommentRepository;
    private readonly ProductCommentBusinessRules _productCommentBusinessRules;

    public ProductCommentManager(IProductCommentRepository productCommentRepository, ProductCommentBusinessRules productCommentBusinessRules)
    {
        _productCommentRepository = productCommentRepository;
        _productCommentBusinessRules = productCommentBusinessRules;
    }

    public async Task<ProductComment?> GetAsync(
        Expression<Func<ProductComment, bool>> predicate,
        Func<IQueryable<ProductComment>, IIncludableQueryable<ProductComment, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ProductComment? productComment = await _productCommentRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return productComment;
    }

    public async Task<IPaginate<ProductComment>?> GetListAsync(
        Expression<Func<ProductComment, bool>>? predicate = null,
        Func<IQueryable<ProductComment>, IOrderedQueryable<ProductComment>>? orderBy = null,
        Func<IQueryable<ProductComment>, IIncludableQueryable<ProductComment, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ProductComment> productCommentList = await _productCommentRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return productCommentList;
    }

    public async Task<ProductComment> AddAsync(ProductComment productComment)
    {
        ProductComment addedProductComment = await _productCommentRepository.AddAsync(productComment);

        return addedProductComment;
    }

    public async Task<ProductComment> UpdateAsync(ProductComment productComment)
    {
        ProductComment updatedProductComment = await _productCommentRepository.UpdateAsync(productComment);

        return updatedProductComment;
    }

    public async Task<ProductComment> DeleteAsync(ProductComment productComment, bool permanent = false)
    {
        ProductComment deletedProductComment = await _productCommentRepository.DeleteAsync(productComment);

        return deletedProductComment;
    }
}
