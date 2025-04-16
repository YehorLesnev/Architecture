using ApplicationCore.Observer.Interfaces;

namespace ApplicationCore.Services.Interfaces;

public interface IMediator
{
	Task SendAsync<TRequest>(TRequest request) where TRequest : IRequest;

	Task<TResult?> SendAsync<TRequest, TResult>(TRequest request) where TRequest : IRequest<TResult>;

	Task<TResult?> QueryAsync<TResult>(IRequest<TResult> query);

	Task<IEnumerable<TResult>> QueryListAsync<TResult>(IRequest<IEnumerable<TResult>> query);
}

// Base Request Interface
public interface IRequest { }

// Request with a Response
public interface IRequest<TResult> { }

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