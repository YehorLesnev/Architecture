using ApplicationCore.Helpers;
using ApplicationCore.Models;
using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using ApplicationCore.Services.Services;

namespace ApplicationCore.Services.Implementations;

public class BalanceService(IBalanceRepository repository)
		: BaseService<BalanceModel>(repository), IBalanceService
{
	public async Task UpdateBalanceDaysOnRequestUpdate(RequestModel request, RequestModel oldRequest)
	{
		var balance = await repository.GetAsync(r => r.UserId == request.UserId && r.Type == request.Type, asNoTracking: true);

		if (balance is null)
			return;

		var totalDays = BalanceHelper.GetTotalDays(request.DateFrom, request.DateTo);
		var oldTotalDays = BalanceHelper.GetTotalDays(oldRequest.DateFrom, oldRequest.DateTo);

		if(!oldRequest.Type.Equals(request.Type, StringComparison.InvariantCultureIgnoreCase))
		{
			var oldBalance = await repository.GetAsync(r => r.UserId == oldRequest.UserId && r.Type == oldRequest.Type, asNoTracking: true);

			if(oldBalance is null)
				return;

			oldBalance.BalanceAmount += oldTotalDays;
			balance.BalanceAmount -= oldTotalDays;

			repository.Update(oldBalance);
		}

		switch (oldTotalDays - totalDays)
		{
			case 0:
				if (!request.IsApproved)
					balance.BalanceAmount += oldTotalDays;

				break;
			case > 0:
				if (request.IsApproved)
				{
					balance.BalanceAmount += oldTotalDays - totalDays;
				}
				else
				{
					balance.BalanceAmount += oldTotalDays;
				}

				break;
			case < 0:
				if (balance.BalanceAmount < totalDays - oldTotalDays)
					throw new Exception("Not enough days on balance.");

				if (request.IsApproved)
				{
					balance.BalanceAmount -= totalDays - oldTotalDays;
				}
				else
				{
					balance.BalanceAmount += oldTotalDays;
				}

				break;
		}

		repository.Update(balance);			
	}

	public async Task UpdateBalanceDaysOnRequestCreate(RequestModel request)
	{
		var balance = await repository.GetAsync(r => r.UserId == request.UserId && r.Type == request.Type, asNoTracking: true);

		if (balance is null)
			return;

		var totalDays = BalanceHelper.GetTotalDays(request.DateFrom, request.DateTo);

		if (balance.BalanceAmount < totalDays)
			throw new Exception("Not enough days on balance.");

		balance.BalanceAmount -= totalDays;

		repository.Update(balance);
	}

	public async Task UpdateBalanceDaysOnRequestDelete(RequestModel request)
	{
		var balance = await repository.GetAsync(r => r.UserId == request.UserId && r.Type == request.Type, asNoTracking: true);

		if (balance is null)
			return;

		var totalDays = BalanceHelper.GetTotalDays(request.DateFrom, request.DateTo);

		if(!request.IsApproved && DateTime.UtcNow < request.DateTo)
		{
			balance.BalanceAmount += totalDays;
			repository.Update(balance);
		}
	}
}