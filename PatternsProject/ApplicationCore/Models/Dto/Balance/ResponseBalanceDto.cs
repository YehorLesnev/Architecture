using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Dto.Balance;

public class ResponseBalanceDto
{
	public Guid BalanceId { get; set; }
	public string Type { get; set; }
	public decimal BalanceAmount { get; set; }
	public Guid UserId { get; set; }
}
