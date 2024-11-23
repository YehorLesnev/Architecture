using ApplicationCore.CQRS.Commands.Request;
using ApplicationCore.CQRS.Queries.Request;
using ApplicationCore.Models;
using ApplicationCore.Models.Dto.Request;
using ApplicationCore.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers;

[Route("requests")]
public class RequestController(IMediator mediator, IMapper mapper) : BaseController(mediator)
{
	[HttpGet("userId/{userId}")]
	public async Task<IActionResult> GetRequestsByUser([FromRoute] Guid userId)
	{
		var requests = await Mediator.QueryListAsync<RequestModel>(new GetRequestsByUserQuery { UserId = userId });
		return Ok(requests);
	}

	[HttpGet("managerId/{managerId}")]
	public async Task<IActionResult> GetRequestsByManager([FromRoute] Guid managerId)
	{
		var requests = await Mediator.QueryListAsync<RequestModel>(new GetRequestsByManagerQuery { ManagerId = managerId });
		return Ok(requests);
	}

	[HttpPost]
	public async Task<IActionResult> CreateRequest([FromBody] CreateRequestDto dto)
	{	
		var command = mapper.Map<CreateRequestCommand>(dto);
		var result = await Mediator.SendAsync<CreateRequestCommand, RequestModel>(command);

		if(result is null) 
			return NotFound("User or manager with specified id not found");

		return Ok(mapper.Map<ResponseRequestDto>(result));
	}

	[HttpPut("{requestId}")]
	public async Task<IActionResult> UpdateRequest(Guid requestId, [FromBody] UpdateRequestDto dto)
	{
		var command = mapper.Map<UpdateRequestCommand>(dto);
		command.RequestId = requestId;
		var result = await Mediator.SendAsync<UpdateRequestCommand, RequestModel>(command);

		if(result is null) 
			return NotFound("Request or manager with specified id not found");

		return Ok(mapper.Map<ResponseRequestDto>(result));
	}

	[HttpDelete("{requestId}")]
	public async Task<IActionResult> DeleteRequest(Guid requestId)
	{
		await Mediator.SendAsync(new DeleteRequestCommand { Id = requestId });

		return Ok();
	}
}

