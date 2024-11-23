using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Dto.Notification;

public class ResponseNotificationDto
{
	public Guid NotificationId { get; set; }
	public Guid UserId { get; set; }
	public Guid SenderId { get; set; }
	public string Text { get; set; }
	public DateTime DateCreated { get; set; }
}
