using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public class RequestModel
{
	[Key]
	public Guid Id { get; set; }

	public required string Type { get; set; }

	public required Guid UserId { get; set; }

	public DateTime DateFrom { get; set; }

	public DateTime? DateTo { get; set; }

	public string Status { get; set; }

	public bool IsApproved { get; set; } = false;

	public DateTime DateCreated { get; set; } = DateTime.UtcNow;

	public Guid ManagerId { get; set; }


	public UserModel User { get; set; }

    public UserModel Manager { get; set; }

    public ICollection<CommentModel> Comments { get; set; }

    public ICollection<FileModel> Files { get; set; }
}
