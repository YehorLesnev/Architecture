using ApplicationCore.Observer.Interfaces;
using ApplicationCore.Services.Interfaces;

namespace ApplicationCore.CQRS.Commands.Notification;

public class CreateNotificationCommand : IRequest, INotification
{
	public string Text { get; set; }

    public Guid UserId { get; set; }

    public Guid? SenderId { get; set; }
}
