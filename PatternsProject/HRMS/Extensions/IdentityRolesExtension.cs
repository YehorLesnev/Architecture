using ApplicationCore.Models;
using Microsoft.AspNetCore.Identity;
using static ApplicationCore.Constants.Constants;

namespace HRMS.Extensions;

public static class IdentityRolesExtension
{
	public static async Task AddIdentityRoles(this WebApplication app)
	{
		// Create roles
		using var scope = app.Services.CreateScope();

		var roleManager =
			scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

		var roles = new[] {
				UserRoleNames.Admin,
				UserRoleNames.Manager,
				UserRoleNames.Employee,
				};

		foreach (var role in roles)
		{
			if (false == await roleManager.RoleExistsAsync(role))
				await roleManager.CreateAsync(new IdentityRole<Guid>(role));
		}

		// Create default users
		var userManager =
			scope.ServiceProvider.GetRequiredService<UserManager<UserModel>>();

		string adminEmail = "admin@admin.com";
		string adminPassword = "Password123!";

		if (await userManager.FindByEmailAsync(adminEmail) == null)
		{
			var user = new UserModel
			{
				UserName = "admin",
				Email = adminEmail
			};

			await userManager.CreateAsync(user, adminPassword);

			await userManager.AddToRoleAsync(user, UserRoleNames.Admin);
		}

		string managerEmail = "manager@manager.com";
		string managerPassword = "Password123!";

		if (await userManager.FindByEmailAsync(managerEmail) == null)
		{
			var user = new UserModel
			{
				UserName = "manager",
				IsManager = true,
				Email = managerEmail
			};

			await userManager.CreateAsync(user, managerPassword);

			await userManager.AddToRoleAsync(user, UserRoleNames.Manager);
		}

		string UserModelEmail = "employee@employee.com";
		string UserModelPassword = "Password123!";

		if (await userManager.FindByEmailAsync(UserModelEmail) == null)
		{
			var user = new UserModel
			{
				UserName = "employee",
				Email = UserModelEmail
			};

			await userManager.CreateAsync(user, UserModelPassword);

			await userManager.AddToRoleAsync(user, UserRoleNames.Employee);
		}
	}
}
