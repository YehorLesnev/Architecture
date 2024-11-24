using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.CQRS.Queries.Notification;

public class GetNotificationsByUserQuery : IRequest<IEnumerable<NotificationModel>>
{
	public Guid UserId { get; set; }
}