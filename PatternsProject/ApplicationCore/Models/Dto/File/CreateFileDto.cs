using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Models.Dto.File;

public class CreateFileDto
{
	public required IFormFile FileContent { get; set; }
	public required Guid RequestId { get; set; }
}
