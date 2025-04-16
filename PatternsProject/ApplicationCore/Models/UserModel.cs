using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public class UserModel : IdentityUser<Guid>
{
	public Guid? ManagerId { get; set; }

	public string FullName { get; set; } = string.Empty;

	public string Position { get; set; } = string.Empty;

	public bool IsManager { get; set; } = false;

	public string ProfilePicture { get; set; } = string.Empty;


	public UserModel Manager { get; set; }
	public ICollection<UserModel> Subordinates { get; set; }
	public ICollection<RequestModel> Requests { get; set; }
	public ICollection<NotificationModel> Notifications { get; set; }
}
