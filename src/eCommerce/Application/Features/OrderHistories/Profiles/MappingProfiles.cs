using Application.Features.OrderHistories.Commands.Create;
using Application.Features.OrderHistories.Commands.Delete;
using Application.Features.OrderHistories.Commands.Update;
using Application.Features.OrderHistories.Queries.GetById;
using Application.Features.OrderHistories.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.OrderHistories.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateOrderHistoryCommand, OrderHistory>();
        CreateMap<OrderHistory, CreatedOrderHistoryResponse>();

        CreateMap<UpdateOrderHistoryCommand, OrderHistory>();
        CreateMap<OrderHistory, UpdatedOrderHistoryResponse>();

        CreateMap<DeleteOrderHistoryCommand, OrderHistory>();
        CreateMap<OrderHistory, DeletedOrderHistoryResponse>();

        CreateMap<OrderHistory, GetByIdOrderHistoryResponse>();

        CreateMap<OrderHistory, GetListOrderHistoryListItemDto>();
        CreateMap<IPaginate<OrderHistory>, GetListResponse<GetListOrderHistoryListItemDto>>();
    }
}