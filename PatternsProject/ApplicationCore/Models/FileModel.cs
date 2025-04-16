using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public class FileModel
{
	[Key]
	public Guid FileId { get; set; }

	public required byte[] FileContent { get; set; } 

	public required Guid RequestId { get; set; }


	public RequestModel Request { get; set; }
}
