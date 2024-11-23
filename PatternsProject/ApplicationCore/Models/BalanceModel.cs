using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public class BalanceModel
{
	[Key]
	public Guid BalanceId { get; set; }

	public required string Type { get; set; }

	public required decimal BalanceAmount { get; set; }

	public required Guid UserId { get; set; }


	public UserModel User { get; set; }
}
