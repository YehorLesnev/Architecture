using ApplicationCore.CQRS.Commands.Balance;
using ApplicationCore.CQRS.Queries.Balance;
using ApplicationCore.Models;
using ApplicationCore.Models.Dto.Balance;
using ApplicationCore.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers;

[Route("balances")]
public class BalanceController(IMediator mediator, IMapper mapper) : BaseController(mediator)
{
	[HttpGet("{userId}")]
	public async Task<IActionResult> GetBalancesByUser(Guid userId)
	{
		var balances = await Mediator.QueryListAsync<BalanceModel>(new GetBalancesByUserQuery { UserId = userId });
		return Ok(mapper.Map<IEnumerable<ResponseBalanceDto>>(balances));
	}

	[HttpPut("{balanceId}")]
	public async Task<IActionResult> UpdateBalance(Guid balanceId, [FromBody] UpdateBalanceDto dto)
	{
		var command = mapper.Map<UpdateBalanceCommand>(dto);
		command.BalanceId = balanceId;
		await Mediator.SendAsync(command);
		return Ok();
	}
}
