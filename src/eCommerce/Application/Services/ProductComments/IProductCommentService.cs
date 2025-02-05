using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ProductComments;

public interface IProductCommentService
{
    Task<ProductComment?> GetAsync(
        Expression<Func<ProductComment, bool>> predicate,
        Func<IQueryable<ProductComment>, IIncludableQueryable<ProductComment, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ProductComment>?> GetListAsync(
        Expression<Func<ProductComment, bool>>? predicate = null,
        Func<IQueryable<ProductComment>, IOrderedQueryable<ProductComment>>? orderBy = null,
        Func<IQueryable<ProductComment>, IIncludableQueryable<ProductComment, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ProductComment> AddAsync(ProductComment productComment);
    Task<ProductComment> UpdateAsync(ProductComment productComment);
    Task<ProductComment> DeleteAsync(ProductComment productComment, bool permanent = false);
}
