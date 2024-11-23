using ApplicationCore.Models;
using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using ApplicationCore.Services.Services;

namespace ApplicationCore.Services.Implementations;

public class NotificationService(INotificationRepository repository)
		: BaseService<NotificationModel>(repository), INotificationService
{
}