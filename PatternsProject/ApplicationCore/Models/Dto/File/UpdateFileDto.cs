using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Models.Dto.File;

public record UpdateFileDto
{
	public required IFormFile FileContent { get; set; }
}
