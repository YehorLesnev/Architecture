using ApplicationCore;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Extensions;

public static class MigrationExtensions
{
	public async static Task ApplyMigrations(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();

		var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

		await dbContext.Database.MigrateAsync();
	}
}
