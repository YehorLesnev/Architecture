using ApplicationCore.Models;
using ApplicationCore.Repositories.Interfaces;

namespace ApplicationCore.Repositories.Implementations;


public class NotificationRepository(ApplicationDbContext dbContext)
		: BaseRepository<NotificationModel>(dbContext), INotificationRepository
{
}