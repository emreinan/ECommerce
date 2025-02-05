using Application.Features.ProductComments.Commands.Create;
using Application.Features.ProductComments.Commands.Delete;
using Application.Features.ProductComments.Commands.Update;
using Application.Features.ProductComments.Queries.GetById;
using Application.Features.ProductComments.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.ProductComments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateProductCommentCommand, ProductComment>();
        CreateMap<ProductComment, CreatedProductCommentResponse>();

        CreateMap<UpdateProductCommentCommand, ProductComment>();
        CreateMap<ProductComment, UpdatedProductCommentResponse>();

        CreateMap<DeleteProductCommentCommand, ProductComment>();
        CreateMap<ProductComment, DeletedProductCommentResponse>();

        CreateMap<ProductComment, GetByIdProductCommentResponse>();

        CreateMap<ProductComment, GetListProductCommentListItemDto>();
        CreateMap<IPaginate<ProductComment>, GetListResponse<GetListProductCommentListItemDto>>();
    }
}