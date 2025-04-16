using ApplicationCore.Models;
using ApplicationCore.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static ApplicationCore.Constants.Constants;

namespace ApplicationCore.Repositories.Implementations;

public class UserRepository(ApplicationDbContext dbContext, UserManager<UserModel> userManager)
		: BaseRepository<UserModel>(dbContext), IUserRepository
{
	public override IEnumerable<UserModel> GetAll(Expression<Func<UserModel, bool>>? filter = null,
		int? pageNumber = null,
		int? pageSize = null,
		bool asNoTracking = false)
	{
		IQueryable<UserModel> query = DbSet;

		if (filter is not null)
			query = query.Where(filter);

		if (pageNumber is null || pageSize is null)
			return asNoTracking ? query.AsNoTracking() : query;

		query = query.OrderBy(x => x.FullName);

		return asNoTracking ? query
				.Skip((pageNumber.Value - 1) * pageSize.Value)
				.Take(pageSize.Value)
				.AsNoTracking()
			: query
				.Skip((pageNumber.Value - 1) * pageSize.Value)
				.Take(pageSize.Value);
	}

	public async Task CreateAsync(UserModel entity, string password)
	{
		var normalizedEmail = entity.Email?.ToUpper();

		if (string.IsNullOrEmpty(normalizedEmail))
			return;

		entity.NormalizedEmail = normalizedEmail;
		entity.UserName = entity.Email;
		entity.NormalizedUserName = normalizedEmail;

		await userManager.CreateAsync(entity, password);
		await userManager.AddToRoleAsync(entity, UserRoleNames.Employee);
	}

	public async Task CreateAsync(UserModel entity, string password, string role)
	{
		var normalizedEmail = entity.Email?.ToUpper();

		if (string.IsNullOrEmpty(normalizedEmail))
			return;

		entity.NormalizedEmail = normalizedEmail;
		entity.UserName = entity.Email;
		entity.NormalizedUserName = normalizedEmail;

		await userManager.CreateAsync(entity, password);

		if (string.IsNullOrEmpty(role))
			await userManager.AddToRoleAsync(entity, role);
	}

	public override async Task CreateAsync(UserModel entity)
	{
		var normalizedEmail = entity.Email?.ToUpper();

		if (string.IsNullOrEmpty(normalizedEmail))
			return;

		entity.NormalizedEmail = normalizedEmail;
		entity.UserName = entity.Email;
		entity.NormalizedUserName = normalizedEmail;

		if ((await userManager.CreateAsync(entity, Constants.Constants.Identity.DefaultUserPassword)).Succeeded)
		{
			await userManager.AddToRoleAsync(entity, UserRoleNames.Employee);
		}
	}

	public override async Task CreateAllAsync(IEnumerable<UserModel> entities)
	{
		foreach (var entity in entities)
		{
			var normalizedEmail = entity.Email.ToUpper();

			entity.NormalizedEmail = normalizedEmail;
			entity.UserName = entity.Email;
			entity.NormalizedUserName = normalizedEmail;

			if ((await userManager.CreateAsync(entity, Constants.Constants.Identity.DefaultUserPassword)).Succeeded)
			{
				await userManager.AddToRoleAsync(entity, UserRoleNames.Employee);
			}
		}
	}

	public async Task CreateAllAsync(IEnumerable<UserModel> entities, string usersPassword)
	{
		foreach (var entity in entities)
		{
			var normalizedEmail = entity.Email.ToUpper();

			entity.NormalizedEmail = normalizedEmail;
			entity.UserName = entity.Email;
			entity.NormalizedUserName = normalizedEmail;

			if ((await userManager.CreateAsync(entity, usersPassword)).Succeeded)
			{
				await userManager.AddToRoleAsync(entity, UserRoleNames.Employee);
			}
		}
	}

	public async Task UpdateAsync(UserModel entity)
	{
		await userManager.UpdateAsync(entity);
		await userManager.UpdateSecurityStampAsync(entity);
		await userManager.UpdateNormalizedEmailAsync(entity);
		await userManager.UpdateNormalizedUserNameAsync(entity);
	}
}