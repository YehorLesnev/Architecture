namespace ApplicationCore.Observer.Interfaces;

public interface IObserver
{
    Task UpdateAsync(INotification notification);
}
