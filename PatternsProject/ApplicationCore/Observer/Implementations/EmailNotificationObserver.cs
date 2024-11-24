using ApplicationCore.Observer.Interfaces;

namespace ApplicationCore.Observer.Implementations;

public class EmailNotificationObserver : IObserver
{
    public async Task UpdateAsync(INotification notification)
    {
        // Simulate sending an email
        await Task.Run(() => Console.WriteLine($"Email sent to User {notification.UserId}: {notification.Text}"));
    }
}