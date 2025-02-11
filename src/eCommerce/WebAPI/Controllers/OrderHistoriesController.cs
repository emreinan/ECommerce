using Application.Features.OrderHistories.Commands.Create;
using Application.Features.OrderHistories.Commands.Delete;
using Application.Features.OrderHistories.Commands.Update;
using Application.Features.OrderHistories.Queries.GetById;
using Application.Features.OrderHistories.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderHistoriesController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedOrderHistoryResponse>> Add([FromBody] CreateOrderHistoryCommand command)
    {
        CreatedOrderHistoryResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedOrderHistoryResponse>> Update([FromBody] UpdateOrderHistoryCommand command)
    {
        UpdatedOrderHistoryResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedOrderHistoryResponse>> Delete([FromRoute] Guid id)
    {
        DeleteOrderHistoryCommand command = new() { Id = id };

        DeletedOrderHistoryResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdOrderHistoryResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdOrderHistoryQuery query = new() { Id = id };

        GetByIdOrderHistoryResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListOrderHistoryListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListOrderHistoryQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListOrderHistoryListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}