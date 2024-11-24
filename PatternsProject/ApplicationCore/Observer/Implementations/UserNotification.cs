using ApplicationCore.Observer.Interfaces;

namespace ApplicationCore.Observer.Implementations;

public class UserNotification : INotification
{
    public string Text { get; }

    public Guid UserId { get; }

    public Guid? SenderId { get; }

    public UserNotification(string message, Guid userId, Guid? senderId)
    {
        Text = message;
        UserId = userId;
		SenderId = senderId;
	}
}