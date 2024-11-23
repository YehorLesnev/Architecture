using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public class CommentModel
{
	[Key]
	public Guid CommentId { get; set; }

	public required string CommentText { get; set; }

	public required Guid RequestId { get; set; }

	public required Guid UserId { get; set; }

	public DateTime DateTimeCreated { get; set; }


	public RequestModel Request { get; set; }
	public UserModel User { get; set; }
}
