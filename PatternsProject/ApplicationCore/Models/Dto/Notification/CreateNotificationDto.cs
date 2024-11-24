
using ApplicationCore.Observer.Interfaces;

namespace ApplicationCore.Models.Dto.Notification;

public class CreateNotificationDto : INotification
{
	public Guid UserId { get; set; }
	public Guid? SenderId { get; set; }
	public string Text { get; set; }
}