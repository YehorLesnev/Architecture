using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Models.Dto.Notification;
using ApplicationCore.CQRS.Queries.Notification;
using ApplicationCore.CQRS.Commands.Notification;

namespace HRMS.Controllers;

[Route("notifications")]
public class NotificationController(IMediator mediator, IMapper mapper) : BaseController(mediator)
{
	[HttpGet("{userId}")]
	public async Task<IActionResult> GetAllByUserId(Guid userId)
	{
		var comments = await Mediator.QueryListAsync<NotificationModel>(new GetNotificationsByUserQuery { UserId = userId });

		return Ok(mapper.Map<IEnumerable<ResponseNotificationDto>>(comments));
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateNotificationDto dto)
	{
		var command = mapper.Map<CreateNotificationCommand>(dto);

		await Mediator.SendAsync(command);

		return Ok();
	}
}