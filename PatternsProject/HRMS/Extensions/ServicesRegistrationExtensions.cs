using ApplicationCore.Repositories.Implementations;
using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Implementations;
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

		return serviceCollection;
	}
}
