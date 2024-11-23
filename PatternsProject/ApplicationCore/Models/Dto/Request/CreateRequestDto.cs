using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Dto.Request;

public class CreateRequestDto
{
	public string Type { get; set; }
	public Guid UserId { get; set; }
	public DateTime DateFrom { get; set; }
	public DateTime? DateTo { get; set; }
	public string Status { get; set; }
	public Guid ManagerId { get; set; }
}
