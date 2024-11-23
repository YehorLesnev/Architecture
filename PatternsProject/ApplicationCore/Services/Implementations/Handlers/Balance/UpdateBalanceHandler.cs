using ApplicationCore.CQRS.Commands.Balance;
using ApplicationCore.CQRS.Commands.Comment;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;
using AutoMapper;

namespace ApplicationCore.Services.Implementations.Handlers.Balance;

public class UpdateBalanceHandler(IBalanceService service, IUserService userService, IMapper mapper) : IRequestHandler<UpdateBalanceCommand>
{
	public async Task HandleAsync(UpdateBalanceCommand command)
	{
		if(await userService.GetAsync(u => u.Id == command.UserId, asNoTracking: true) is null || 
			await service.GetAsync(r => r.BalanceId == command.BalanceId, asNoTracking: true) is null)
				throw new Exception("Couldn't update balance. User or balance not found.");

		await service.CreateAsync(mapper.Map<BalanceModel>(command));
	}
}
