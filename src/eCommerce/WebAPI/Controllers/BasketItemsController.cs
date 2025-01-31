using Application.Features.BasketItems.Commands.Create;
using Application.Features.BasketItems.Commands.Delete;
using Application.Features.BasketItems.Commands.Update;
using Application.Features.BasketItems.Queries.GetById;
using Application.Features.BasketItems.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketItemsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedBasketItemResponse>> Add([FromBody] CreateBasketItemCommand command)
    {
        CreatedBasketItemResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedBasketItemResponse>> Update([FromBody] UpdateBasketItemCommand command)
    {
        UpdatedBasketItemResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedBasketItemResponse>> Delete([FromRoute] Guid id)
    {
        DeleteBasketItemCommand command = new() { Id = id };

        DeletedBasketItemResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdBasketItemResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdBasketItemQuery query = new() { Id = id };

        GetByIdBasketItemResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListBasketItemListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBasketItemQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListBasketItemListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}