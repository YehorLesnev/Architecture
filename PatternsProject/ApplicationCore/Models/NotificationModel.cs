using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public class NotificationModel
{
    [Key]
    public Guid NotificationId { get; set; }

    public required Guid UserId { get; set; }

    public required Guid SenderId { get; set; }

    public required string Text { get; set; }

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
	
    public UserModel User { get; set; }
    public UserModel Sender { get; set; }
}
