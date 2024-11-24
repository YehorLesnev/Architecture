using ApplicationCore.CQRS.Queries.Notification;
using ApplicationCore.Models;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.Services.Implementations.Handlers.Notification;

public class GetNotificationsByUserHandler(INotificationService notificationService) : IRequestHandler<GetNotificationsByUserQuery, IEnumerable<NotificationModel>>
{
	public async Task<IEnumerable<NotificationModel>?> HandleAsync(GetNotificationsByUserQuery request)
	{
		return notificationService.GetAll(x => x.UserId == request.UserId);
	}
}
