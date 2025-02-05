using Application.Features.Discounts.Commands.Create;
using Application.Features.Discounts.Commands.Delete;
using Application.Features.Discounts.Commands.Update;
using Application.Features.Discounts.Queries.GetById;
using Application.Features.Discounts.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Discounts.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateDiscountCommand, Discount>();
        CreateMap<Discount, CreatedDiscountResponse>();

        CreateMap<UpdateDiscountCommand, Discount>();
        CreateMap<Discount, UpdatedDiscountResponse>();

        CreateMap<DeleteDiscountCommand, Discount>();
        CreateMap<Discount, DeletedDiscountResponse>();

        CreateMap<Discount, GetByIdDiscountResponse>();

        CreateMap<Discount, GetListDiscountListItemDto>();
        CreateMap<IPaginate<Discount>, GetListResponse<GetListDiscountListItemDto>>();
    }
}