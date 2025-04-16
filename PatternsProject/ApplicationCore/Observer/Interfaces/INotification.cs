namespace ApplicationCore.Observer.Interfaces;

public interface INotification
{
    string Text { get; }

    Guid UserId { get; }

    Guid? SenderId { get; }
}
