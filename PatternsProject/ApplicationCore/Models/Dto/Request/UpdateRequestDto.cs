namespace ApplicationCore.Models.Dto.Request;

public class UpdateRequestDto
{
	public string Type { get; set; }
	public DateTime DateFrom { get; set; }
	public DateTime? DateTo { get; set; }
	public string Status { get; set; }
	public bool IsApproved { get; set; }
	public Guid ManagerId { get; set; }
}
