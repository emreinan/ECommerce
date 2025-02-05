using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ProductCommentRepository : EfRepositoryBase<ProductComment, Guid, BaseDbContext>, IProductCommentRepository
{
    public ProductCommentRepository(BaseDbContext context) : base(context)
    {
    }
}