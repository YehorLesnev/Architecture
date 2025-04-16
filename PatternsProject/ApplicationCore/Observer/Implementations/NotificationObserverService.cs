using ApplicationCore.Observer.Interfaces;
using ApplicationCore.Services.Interfaces;
using AutoMapper;

namespace ApplicationCore.Observer.Implementations;

public class NotificationObserverService
{
    private readonly ISubject _subject;

    public NotificationObserverService(ISubject subject, INotificationService notificationService, IMapper mapper)
    {
        _subject = subject;

        _subject.Attach(new EmailNotificationObserver());
        _subject.Attach(new InAppObserver(notificationService, mapper));
    }

    public async Task NotifyAllAsync(INotification notification)
    {
        await _subject.NotifyAsync(notification);
    }
}
