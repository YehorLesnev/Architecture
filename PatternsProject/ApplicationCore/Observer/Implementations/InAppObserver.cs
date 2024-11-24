using ApplicationCore.Models;
using ApplicationCore.Observer.Interfaces;
using ApplicationCore.Services.Interfaces;
using AutoMapper;

namespace ApplicationCore.Observer.Implementations;

public class InAppObserver(INotificationService notificationService, IMapper mapper) : IObserver
{
    public async Task UpdateAsync(INotification notification)
    {
        var notificationModel = mapper.Map<NotificationModel>(notification);

		await notificationService.CreateAsync(notificationModel);
	}
}
