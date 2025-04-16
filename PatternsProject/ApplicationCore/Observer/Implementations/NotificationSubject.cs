using ApplicationCore.Observer.Interfaces;

namespace ApplicationCore.Observer.Implementations;

public class NotificationSubject : ISubject
{
    private readonly List<IObserver> _observers = [];

    public void Attach(IObserver observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public void Detach(IObserver observer)
    {
		_observers.Remove(observer);
	}

    public async Task NotifyAsync(INotification notification)
    {
        foreach (var observer in _observers)
        {
            await observer.UpdateAsync(notification);
        }
    }
}
