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
				BalanceAmount = Constants.Constants.BalanceConstants.DefaultVacationDays
			},
			new()
			{
				UserId = userId,
				Type = Constants.Constants.BalanceTypeNames.SickLeave,
				BalanceAmount = Constants.Constants.BalanceConstants.DefaultSickLeaveDays
			},
			new()
			{
				UserId = userId,
				Type = Constants.Constants.BalanceTypeNames.SickLeaveWithAttachments,
				BalanceAmount = Constants.Constants.BalanceConstants.DefaultSickLeaveWithAttachmentsDays
			},
		];

	public static decimal GetTotalDays( DateTime dateFrom, DateTime dateTo)
	{
		if(dateTo == null || dateFrom == null)
			return 0;

		decimal totalDays = (decimal)((TimeSpan)(dateTo - dateFrom)).TotalDays;
		return Math.Round(totalDays * 2, MidpointRounding.AwayFromZero) / 2;
	}
}
