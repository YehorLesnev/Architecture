namespace ApplicationCore.Services.Interfaces;

public interface IMediator
{
	Task SendAsync<TRequest>(TRequest request) where TRequest : IRequest;

	Task<TResult?> SendAsync<TRequest, TResult>(TRequest request) where TRequest : IRequest<TResult>;

	Task PublishAsync<TNotification>(TNotification notification) where TNotification : INotification;

	Task<TResult?> QueryAsync<TResult>(IRequest<TResult> query);

	Task<IEnumerable<TResult>> QueryListAsync<TResult>(IRequest<IEnumerable<TResult>> query);
}

// Base Request Interface
public interface IRequest { }

// Request with a Response
public interface IRequest<TResult> { }

// Notification (for broadcasting)
public interface INotification { }

// Command/Request Handler
public interface IRequestHandler<TRequest> where TRequest : IRequest
{
    Task HandleAsync(TRequest request);
}

// Query Handler (Request with Result)
public interface IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
{
    Task<TResult?> HandleAsync(TRequest request);
}

// Notification Handler
public interface INotificationHandler<TNotification> where TNotification : INotification
{
    Task HandleAsync(TNotification notification);
}
