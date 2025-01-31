using Application.Features.BasketItems.Commands.Create;
using Application.Features.BasketItems.Commands.Delete;
using Application.Features.BasketItems.Commands.Update;
using Application.Features.BasketItems.Queries.GetById;
using Application.Features.BasketItems.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.BasketItems.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateBasketItemCommand, BasketItem>();
        CreateMap<BasketItem, CreatedBasketItemResponse>();

        CreateMap<UpdateBasketItemCommand, BasketItem>();
        CreateMap<BasketItem, UpdatedBasketItemResponse>();

        CreateMap<DeleteBasketItemCommand, BasketItem>();
        CreateMap<BasketItem, DeletedBasketItemResponse>();

        CreateMap<BasketItem, GetByIdBasketItemResponse>();

        CreateMap<BasketItem, GetListBasketItemListItemDto>();
        CreateMap<IPaginate<BasketItem>, GetListResponse<GetListBasketItemListItemDto>>();
    }
}