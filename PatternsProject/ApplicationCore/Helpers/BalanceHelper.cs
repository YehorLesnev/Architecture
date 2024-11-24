using ApplicationCore.Models;

namespace ApplicationCore.Helpers;

public static class BalanceHelper
{
	public static IEnumerable<BalanceModel> GetDefaultUserBalances(Guid userId)
		=> 
		[
			new()
			{
				UserId = userId,
				Type = Constants.Constants.BalanceTypeNames.Vacation,
				BalanceAmount = 0.0m
			},
			new()
			{
				UserId = userId,
				Type = Constants.Constants.BalanceTypeNames.SickLeave,
				BalanceAmount = 0.0m
			},
		];
}
