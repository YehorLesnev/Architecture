using ApplicationCore.CQRS.Commands.Balance;
using ApplicationCore.Helpers;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.Services.Implementations.Handlers.Balance;

public class CreateUserBalanceHandler(IBalanceService balanceService) : IRequestHandler<CreateUserBalanceCommand>
{
	public async Task HandleAsync(CreateUserBalanceCommand request)
	{
		await balanceService.CreateAllAsync(BalanceHelper.GetDefaultUserBalances(request.UserId));
	}
}
