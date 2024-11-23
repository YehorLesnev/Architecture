namespace ApplicationCore.Models.Dto.Request;

public class CreateRequestDto
{
	public string Type { get; set; }
	public Guid UserId { get; set; }
	public DateTime DateFrom { get; set; }
	public DateTime? DateTo { get; set; }
	public string Status { get; set; } = Constants.Constants.RequestStatusNames.WaitingForApprove;
	public Guid ManagerId { get; set; }
}
