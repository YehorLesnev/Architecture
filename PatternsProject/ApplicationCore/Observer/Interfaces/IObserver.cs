namespace ApplicationCore.Observer;

public interface IObserver
{
    Task UpdateAsync(INotification notification);
}
