namespace ApplicationCore.Models.Dto.File;

public class ResponseFileDto
{
	public Guid FileId { get; set; }
	public string FileContent { get; set; }
	public Guid RequestId { get; set; }
}
