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

		return serviceCollection;
	}

	public static IServiceCollection? RegisterServices(this IServiceCollection? serviceCollection)
	{
		if (serviceCollection is null) return serviceCollection;

		serviceCollection.AddScoped<IUserService, UserService>();

		return serviceCollection;
	}
}
