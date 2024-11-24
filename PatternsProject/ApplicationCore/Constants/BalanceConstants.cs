﻿namespace ApplicationCore.Constants;

public static partial class Constants
{
	public static class BalanceTypeNames
	{
		public const string Vacation = "Vacation";

		public const string SickLeave = "Sick Leave";

		public const string SickLeaveWithAttachments = "Sick leave with medical confirmation";
	}

	public static class BalanceConstants
	{
		public const int DefaultVacationDays = 15;

		public const int DefaultSickLeaveDays = 5;

		public const int DefaultSickLeaveWithAttachmentsDays = 10;
	}
}