using ApplicationCore.CQRS.Commands.Comment;
using ApplicationCore.CQRS.Queries.Comment;
using ApplicationCore.Models;
using ApplicationCore.Models.Dto.Comment;
using ApplicationCore.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers;

[Route("comments")]
public class CommentController(IMediator mediator, IMapper mapper) : BaseController(mediator)
{
	[HttpGet("{requestId}")]
	public async Task<IActionResult> GetCommentsByRequest(Guid requestId)
	{
		var comments = await Mediator.QueryListAsync<CommentModel>(new GetCommentsByRequestQuery { RequestId = requestId });
		return Ok(mapper.Map<IEnumerable<ResponseCommentDto>>(comments));
	}

	[HttpPost]
	public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto dto)
	{
		var command = mapper.Map<CreateCommentCommand>(dto);

		await Mediator.SendAsync(command);

		return Ok();
	}
}
