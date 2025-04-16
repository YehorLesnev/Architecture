using ApplicationCore.CQRS.Commands.Notification;
using ApplicationCore.Observer.Implementations;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.Services.Implementations.Handlers.Notification;

public class CreateNotificationHandler(NotificationObserverService notificationObserverService, IUserService userService) : IRequestHandler<CreateNotificationCommand>
{
	public async Task HandleAsync(CreateNotificationCommand request)
	{
		if(await userService.GetAsync(x => x.Id == request.UserId, asNoTracking: true) is null)
			throw new Exception($"{nameof(CreateNotificationHandler)}: User not found");

		if(request.SenderId is null || await userService.GetAsync(x => x.Id == request.SenderId, asNoTracking: true) is null)
			request.SenderId = null;

		await notificationObserverService.NotifyAllAsync(request);
	}
}
