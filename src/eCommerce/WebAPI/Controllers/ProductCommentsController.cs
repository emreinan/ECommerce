using Application.Features.ProductComments.Commands.Create;
using Application.Features.ProductComments.Commands.Delete;
using Application.Features.ProductComments.Commands.Update;
using Application.Features.ProductComments.Queries.GetById;
using Application.Features.ProductComments.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductCommentsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedProductCommentResponse>> Add([FromBody] CreateProductCommentCommand command)
    {
        CreatedProductCommentResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedProductCommentResponse>> Update([FromBody] UpdateProductCommentCommand command)
    {
        UpdatedProductCommentResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedProductCommentResponse>> Delete([FromRoute] Guid id)
    {
        DeleteProductCommentCommand command = new() { Id = id };

        DeletedProductCommentResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdProductCommentResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdProductCommentQuery query = new() { Id = id };

        GetByIdProductCommentResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListProductCommentListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListProductCommentQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListProductCommentListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}