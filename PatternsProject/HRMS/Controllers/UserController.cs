using ApplicationCore.CQRS.Queries;
using ApplicationCore.Models;
using ApplicationCore.Models.Dto.User;
using ApplicationCore.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers;

[Route("api/users")]
public class UserController(IMediator mediator, IMapper mapper) : BaseController(mediator)
{
	[HttpGet("{userId}")]
	public async Task<IActionResult> GetUser(Guid userId)
	{
		var user = await Mediator.QueryAsync<UserModel>(new GetUserByIdQuery { UserId = userId });

		if (user is null) return NotFound();

		return Ok(mapper.Map<ResponseUserDto>(user));
	}

	[HttpGet]
	public async Task<IActionResult> GetAllManagerSubordinates([FromQuery] Guid managerId)
	{
		var users = await Mediator.QueryListAsync<UserModel>(new GetUsersByManagerQuery { ManagerId = managerId });
		return Ok(mapper.Map<IEnumerable<ResponseUserDto>>(users));
	}
}