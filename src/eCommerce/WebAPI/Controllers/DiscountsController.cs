using Application.Features.Discounts.Commands.Create;
using Application.Features.Discounts.Commands.Delete;
using Application.Features.Discounts.Commands.Update;
using Application.Features.Discounts.Queries.GetById;
using Application.Features.Discounts.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedDiscountResponse>> Add([FromBody] CreateDiscountCommand command)
    {
        CreatedDiscountResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedDiscountResponse>> Update([FromBody] UpdateDiscountCommand command)
    {
        UpdatedDiscountResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedDiscountResponse>> Delete([FromRoute] Guid id)
    {
        DeleteDiscountCommand command = new() { Id = id };

        DeletedDiscountResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdDiscountResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdDiscountQuery query = new() { Id = id };

        GetByIdDiscountResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListDiscountListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListDiscountQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListDiscountListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}