using ApplicationCore.CQRS.Commands.Request;
using ApplicationCore.CQRS.Queries.Request;
using ApplicationCore.CQRS.Queries.User;
using ApplicationCore.Models;
using ApplicationCore.Repositories.Implementations;
using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Implementations;
using ApplicationCore.Services.Implementations.Handlers.Request;
using ApplicationCore.Services.Implementations.Handlers.User;
using ApplicationCore.Services.Interfaces;

namespace HRMS.Extensions;

public static class ServicesRegistrationExtensions
{
	public static IServiceCollection? RegisterRepositories(this IServiceCollection? serviceCollection)
	{
		if (serviceCollection is null) return serviceCollection;

		serviceCollection.AddScoped<IUserRepository, UserRepository>();
		serviceCollection.AddScoped<IBalanceRepository, BalanceRepository>();
		serviceCollection.AddScoped<IRequestRepository, RequestRepository>();
		serviceCollection.AddScoped<INotificationRepository, NotificationRepository>();
		serviceCollection.AddScoped<IFileRepository, FileRepository>();
		serviceCollection.AddScoped<ICommentRepository, CommentRepository>();

		return serviceCollection;
	}

	public static IServiceCollection? RegisterServices(this IServiceCollection? serviceCollection)
	{
		if (serviceCollection is null) return serviceCollection;

		serviceCollection.AddScoped<IUserService, UserService>();
		serviceCollection.AddScoped<IBalanceService, BalanceService>();
		serviceCollection.AddScoped<IRequestService, RequestService>();
		serviceCollection.AddScoped<INotificationService, NotificationService>();
		serviceCollection.AddScoped<IFileService, FileService>();
		serviceCollection.AddScoped<ICommentService, CommentService>();

		//handlers
		serviceCollection.AddScoped<IMediator, Mediator>();

		serviceCollection.AddScoped<IRequestHandler<GetUserByIdQuery, UserModel>, GetUserByIdHandler>();
		serviceCollection.AddScoped<IRequestHandler<GetUsersByManagerQuery, IEnumerable<UserModel>>, GetUsersByManagerHandler>();

		serviceCollection.AddScoped< IRequestHandler<GetRequestsByUserQuery, IEnumerable<RequestModel>>, GetRequestsByUserHandler>();
		serviceCollection.AddScoped<IRequestHandler<GetRequestsByManagerQuery, IEnumerable<RequestModel>>, GetRequestsByManagerHandler>();

		serviceCollection.AddScoped<IRequestHandler<CreateRequestCommand, RequestModel>, CreateRequestHandler>();
		serviceCollection.AddScoped<IRequestHandler<UpdateRequestCommand, RequestModel>, UpdateRequestHandler>();
		serviceCollection.AddScoped<IRequestHandler<DeleteRequestCommand>, DeleteRequestHandler>();

		return serviceCollection;
	}
}
