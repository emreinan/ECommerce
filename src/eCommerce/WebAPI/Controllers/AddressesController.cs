using Application.Features.Addresses.Commands.Create;
using Application.Features.Addresses.Commands.Delete;
using Application.Features.Addresses.Commands.Update;
using Application.Features.Addresses.Queries.GetById;
using Application.Features.Addresses.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressesController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedAddressResponse>> Add([FromBody] CreateAddressCommand command)
    {
        CreatedAddressResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedAddressResponse>> Update([FromBody] UpdateAddressCommand command)
    {
        UpdatedAddressResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedAddressResponse>> Delete([FromRoute] Guid id)
    {
        DeleteAddressCommand command = new() { Id = id };

        DeletedAddressResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdAddressResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdAddressQuery query = new() { Id = id };

        GetByIdAddressResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListAddressListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListAddressQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListAddressListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}