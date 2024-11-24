using ApplicationCore.Observer.Interfaces;
using ApplicationCore.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationCore.Services.Implementations;

public class Mediator(IServiceProvider serviceProvider) : IMediator
{
	public async Task SendAsync<TRequest>(TRequest request) where TRequest : IRequest
	{
		var handler = serviceProvider.GetService(typeof(IRequestHandler<TRequest>)) as IRequestHandler<TRequest> ?? throw new InvalidOperationException($"No handler found for {typeof(TRequest).Name}");

		await handler.HandleAsync(request);
	}

	public async Task<TResult?> SendAsync<TRequest, TResult>(TRequest request) where TRequest : IRequest<TResult>
	{
		return serviceProvider.GetService(typeof(IRequestHandler<TRequest, TResult>)) is not IRequestHandler<TRequest, TResult> handler
			? throw new InvalidOperationException($"No handler found for {typeof(TRequest).Name}")
			: await handler.HandleAsync(request);
	}

	public async Task<TResult?> QueryAsync<TResult>(IRequest<TResult> query)
	{
		var handlerType = typeof(IRequestHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));

		var handler = serviceProvider.GetService(handlerType);

		return handler == null
			? throw new InvalidOperationException($"No handler found for {query.GetType().Name}")
			: (TResult)await ((dynamic)handler).HandleAsync((dynamic)query);
	}

	public async Task<IEnumerable<TResult>> QueryListAsync<TResult>(IRequest<IEnumerable<TResult>> query)
	{
		var handlerType = typeof(IRequestHandler<,>).MakeGenericType(query.GetType(), typeof(IEnumerable<TResult>));

		var handler = serviceProvider.GetService(handlerType);

		return handler == null
			? throw new InvalidOperationException($"No handler found for {query.GetType().Name}")
			: (IEnumerable<TResult>)await ((dynamic)handler).HandleAsync((dynamic)query);
	}
}
